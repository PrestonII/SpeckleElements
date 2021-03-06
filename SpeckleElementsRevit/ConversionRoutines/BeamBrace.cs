﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using SpeckleCore;
using SpeckleCore.Data;
using SpeckleCoreGeometryClasses;
using SpeckleElementsClasses;

namespace SpeckleElementsRevit
{

  public static partial class Conversions
  {
    //Trying to reduce code duplication, beam and brace are basically the same elements in Revit
    public static Element ToNative(this Brace myBrace)
    {
      var myBeam = new Beam()
      {
        beamFamily = myBrace.braceFamily,
        beamType = myBrace.braceType,
        baseLine = myBrace.baseLine,
        parameters = myBrace.parameters,
        level = myBrace.level
      };

      return StructuralFramingToNative(myBeam, StructuralType.Brace);
    }

    //Trying to reduce code duplication, beam and brace are basically the same elements in Revit
    public static SpeckleObject BraceToSpeckle(Autodesk.Revit.DB.FamilyInstance myFamily)
    {
      var myBeam = BeamToSpeckle(myFamily) as Beam;

      var myBrace = new Brace()
      {
        braceFamily = myBeam.beamFamily,
        braceType = myBeam.beamType,
        baseLine = myBeam.baseLine,
        parameters = myBeam.parameters,
        level = myBeam.level
      };


      return myBrace;
    }

    public static Element ToNative(this Beam myBeam)
    {
      return StructuralFramingToNative(myBeam, StructuralType.Beam);
    }


    public static Element StructuralFramingToNative(Beam myBeam, StructuralType structuralType)
    {
      var (docObj, stateObj) = GetExistingElementByApplicationId( myBeam.ApplicationId, myBeam.Type );
      try
      {
        var baseLine = (Autodesk.Revit.DB.Curve) SpeckleCore.Converter.Deserialise( obj: myBeam.baseLine, excludeAssebmlies: new string[ ] { "SpeckleCoreGeometryDynamo" } );


        var start = baseLine.GetEndPoint( 0 );
        var end = baseLine.GetEndPoint( 1 );

        FamilySymbol familySymbol;
        familySymbol = GetFamilySymbolByFamilyNameAndTypeAndCategory( myBeam.beamFamily, myBeam.beamType, BuiltInCategory.OST_StructuralFraming );

        if( familySymbol == null )
          familySymbol = GetFamilySymbolByFamilyNameAndTypeAndCategory( myBeam.beamFamily, myBeam.beamType, BuiltInCategory.OST_BeamAnalytical );

        // Freak out if we don't have a symbol.
        if( familySymbol == null )
        {
          ConversionErrors.Add(new SpeckleConversionError { Message = $"Missing family: {myBeam.beamFamily} {myBeam.beamType}" });
          throw new RevitFamilyNotFoundException($"No 'Beam' family found in the project");
        }

        // Activate the symbol yo! 
        if ( !familySymbol.IsActive ) familySymbol.Activate();

        // If we have an existing element we can edit: 
        if( docObj != null )
        {
          var type = Doc.GetElement( docObj.GetTypeId() ) as ElementType;

          // if family changed, tough luck. delete and let us create a new one.
          if( myBeam.beamFamily != type.FamilyName )
          {
            Doc.Delete( docObj.Id );
          }
          else
          {
            var existingFamilyInstance = (Autodesk.Revit.DB.FamilyInstance) docObj;
            var existingLocationCurve = existingFamilyInstance.Location as LocationCurve;
            existingLocationCurve.Curve = baseLine;

            // check for a type change
            if( myBeam.beamType != null && myBeam.beamType != type.Name )
            {
              existingFamilyInstance.ChangeTypeId( familySymbol.Id );
            }

            SetElementParams( existingFamilyInstance, myBeam.parameters );
            return existingFamilyInstance;
          }
        }

        if( myBeam.level == null )
          myBeam.level = new SpeckleElementsClasses.Level() { elevation = 0, levelName = "Speckle Level 0" };
        var myLevel = myBeam.level.ToNative() as Autodesk.Revit.DB.Level;

        var familyInstance = Doc.Create.NewFamilyInstance( baseLine, familySymbol, myLevel, structuralType);


        SetElementParams( familyInstance, myBeam.parameters );
        return familyInstance;
      }
      catch( Exception e )
      {
        return null;
      }
    }

    public static SpeckleObject BeamToSpeckle( Autodesk.Revit.DB.FamilyInstance myFamily )
    {
      // Generate Beam
      var myBeam = new Beam();
      var allSolids = GetElementSolids( myFamily, opt: new Options() { DetailLevel = ViewDetailLevel.Fine, ComputeReferences = true } );

      (myBeam.Faces, myBeam.Vertices) = GetFaceVertexArrFromSolids( allSolids );
      var baseCurve = myFamily.Location as LocationCurve;
      myBeam.baseLine = (SpeckleCoreGeometryClasses.SpeckleLine) SpeckleCore.Converter.Serialise( baseCurve.Curve );

      myBeam.beamFamily = myFamily.Symbol.FamilyName;
      myBeam.beamType = Doc.GetElement( myFamily.GetTypeId() ).Name;

      myBeam.parameters = GetElementParams( myFamily );

      //myFamily.just

      myBeam.GenerateHash();
      myBeam.ApplicationId = myFamily.UniqueId;

      //var analyticalModel = AnalyticalStickToSpeckle(myFamily);

      return myBeam;//.Concat(analyticalModel).ToList();
    }
  }
}
