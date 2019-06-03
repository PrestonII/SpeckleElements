﻿using SpeckleCore;
using SpeckleCoreGeometryClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace SpeckleElements
{
    #region Helper objects
    public partial class StructuralVectorThree
    {
        public StructuralVectorThree() { }

        public StructuralVectorThree(double[] value, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Value = value.ToList();
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public StructuralVectorThree(double x, double y, double z, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Value = new List<double>(new double[] { x, y, z });
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            for (int i = 0; i < this.Value.Count(); i++)
                this.Value[i] *= factor;

            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }

        public void TransformOntoAxis(StructuralAxis axis)
        {
            double x = 0, y = 0, z = 0;

            x += axis.Xdir.Value[0] * Value[0];
            y += axis.Xdir.Value[1] * Value[0];
            z += axis.Xdir.Value[2] * Value[0];

            x += axis.Ydir.Value[0] * Value[1];
            y += axis.Ydir.Value[1] * Value[1];
            z += axis.Ydir.Value[2] * Value[1];

            x += axis.Normal.Value[0] * Value[2];
            y += axis.Normal.Value[1] * Value[2];
            z += axis.Normal.Value[2] * Value[2];

            this.Value = new List<double>(new double[] { x, y, z });
            GenerateHash();
        }
    }

    public partial class StructuralVectorBoolThree
    {
        public StructuralVectorBoolThree() { }

        public StructuralVectorBoolThree(bool[] value, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Value = value.ToList();
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public StructuralVectorBoolThree(bool x, bool y, bool z, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Value = new List<bool>(new bool[] { x, y, z });
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            return;
        }
    }

    public partial class StructuralVectorSix
    {
        public StructuralVectorSix() { }

        public StructuralVectorSix(double[] value, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Value = value.ToList();
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public StructuralVectorSix(double x, double y, double z, double xx, double yy, double zz, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Value = new List<double>(new double[] { x, y, z, xx, yy, zz });
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            for (int i = 0; i < this.Value.Count(); i++)
                this.Value[i] *= factor;

            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }

        public void TransformOntoAxis(StructuralAxis axis)
        {
            double x = 0, y = 0, z = 0, xx = 0, yy = 0, zz = 0;

            x += axis.Xdir.Value[0] * Value[0];
            y += axis.Xdir.Value[1] * Value[0];
            z += axis.Xdir.Value[2] * Value[0];
            xx += axis.Xdir.Value[0] * Value[0];
            yy += axis.Xdir.Value[1] * Value[0];
            zz += axis.Xdir.Value[2] * Value[0];

            x += axis.Ydir.Value[0] * Value[1];
            y += axis.Ydir.Value[1] * Value[1];
            z += axis.Ydir.Value[2] * Value[1];
            xx += axis.Ydir.Value[0] * Value[0];
            yy += axis.Ydir.Value[1] * Value[0];
            zz += axis.Ydir.Value[2] * Value[0];

            x += axis.Normal.Value[0] * Value[2];
            y += axis.Normal.Value[1] * Value[2];
            z += axis.Normal.Value[2] * Value[2];
            xx += axis.Normal.Value[0] * Value[0];
            yy += axis.Normal.Value[1] * Value[0];
            zz += axis.Normal.Value[2] * Value[0];

            this.Value = new List<double>(new double[] { x, y, z, xx, yy, zz });
            GenerateHash();
        }
    }

    public partial class StructuralVectorBoolSix
    {
        public StructuralVectorBoolSix() { }

        public StructuralVectorBoolSix(bool[] value, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Value = value.ToList();
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public StructuralVectorBoolSix(bool x, bool y, bool z, bool xx, bool yy, bool zz, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Value = new List<bool>(new bool[] { x, y, z, xx, yy, zz });
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            return;
        }
    }

    public partial class StructuralAxis
    {
        public StructuralAxis() { }

        public StructuralAxis(StructuralVectorThree xdir, StructuralVectorThree ydir, StructuralVectorThree normal, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Normal = normal;
            this.Xdir = xdir;
            this.Ydir = ydir;
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public StructuralAxis(StructuralVectorThree xdir, StructuralVectorThree ydir, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Normal = new StructuralVectorThree(new double[]
            {
                xdir.Value[2] * ydir.Value[3] - xdir.Value[3] * ydir.Value[2],
                xdir.Value[3] * ydir.Value[1] - xdir.Value[1] * ydir.Value[3],
                xdir.Value[1] * ydir.Value[2] - xdir.Value[2] * ydir.Value[1],
            });
            this.Xdir = xdir;
            this.Ydir = ydir;
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Normal.Scale(factor);
            this.Xdir.Scale(factor);
            this.Ydir.Scale(factor);

            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }

        public void Normalize()
        {
            double mag;

            // X
            mag = Math.Sqrt(Xdir.Value.Sum(x => x * x));
            Xdir.Value[0] /= mag;
            Xdir.Value[1] /= mag;
            Xdir.Value[2] /= mag;

            // Y
            mag = Math.Sqrt(Ydir.Value.Sum(y => y * y));
            Ydir.Value[0] /= mag;
            Ydir.Value[1] /= mag;
            Ydir.Value[2] /= mag;

            // Normal
            mag = Math.Sqrt(Normal.Value.Sum(n => n * n));
            Normal.Value[0] /= mag;
            Normal.Value[1] /= mag;
            Normal.Value[2] /= mag;
        }

        public void RotateAxisAboutX(double angle)
        {
            if (angle == 0) return;

            Matrix3D rotationMatrix = RotationMatrix(new Vector3D(Xdir.Value[0], Xdir.Value[1], Xdir.Value[2]), angle);

            Vector3D Y = Vector3D.Multiply(new Vector3D(Ydir.Value[0], Ydir.Value[1], Ydir.Value[2]), rotationMatrix);
            Vector3D Z = Vector3D.Multiply(new Vector3D(Normal.Value[0], Normal.Value[1], Normal.Value[2]), rotationMatrix);

            Ydir = new SpeckleVector(Y.X, Y.Y, Y.Z);
            Normal = new SpeckleVector(Z.X, Z.Y, Z.Z);
        }

        public void RotateAxisAboutY(double angle)
        {
            if (angle == 0) return;

            Matrix3D rotationMatrix = RotationMatrix(new Vector3D(Ydir.Value[0], Ydir.Value[1], Ydir.Value[2]), angle);

            Vector3D X = Vector3D.Multiply(new Vector3D(Xdir.Value[0], Xdir.Value[1], Xdir.Value[2]), rotationMatrix);
            Vector3D Z = Vector3D.Multiply(new Vector3D(Normal.Value[0], Normal.Value[1], Normal.Value[2]), rotationMatrix);

            Xdir = new SpeckleVector(X.X, X.Y, X.Z);
            Normal = new SpeckleVector(Z.X, Z.Y, Z.Z);
        }

        public void RotateAxisAboutZ(double angle)
        {
            if (angle == 0) return;

            Matrix3D rotationMatrix = RotationMatrix(new Vector3D(Normal.Value[0], Normal.Value[1], Normal.Value[2]), angle);

            Vector3D X = Vector3D.Multiply(new Vector3D(Xdir.Value[0], Xdir.Value[1], Xdir.Value[2]), rotationMatrix);
            Vector3D Y = Vector3D.Multiply(new Vector3D(Ydir.Value[0], Ydir.Value[1], Ydir.Value[2]), rotationMatrix);

            Xdir = new SpeckleVector(X.X, X.Y, X.Z);
            Ydir = new SpeckleVector(Y.X, Y.Y, Y.Z);
        }

        private static Matrix3D RotationMatrix(Vector3D zUnitVector, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);

            // TRANSPOSED MATRIX TO ACCOMODATE MULTIPLY FUNCTION
            return new Matrix3D(
                cos + Math.Pow(zUnitVector.X, 2) * (1 - cos),
                zUnitVector.Y * zUnitVector.X * (1 - cos) + zUnitVector.Z * sin,
                zUnitVector.Z * zUnitVector.X * (1 - cos) - zUnitVector.Y * sin,
                0,

                zUnitVector.X * zUnitVector.Y * (1 - cos) - zUnitVector.Z * sin,
                cos + Math.Pow(zUnitVector.Y, 2) * (1 - cos),
                zUnitVector.Z * zUnitVector.Y * (1 - cos) + zUnitVector.X * sin,
                0,

                zUnitVector.X * zUnitVector.Z * (1 - cos) + zUnitVector.Y * sin,
                zUnitVector.Y * zUnitVector.Z * (1 - cos) - zUnitVector.X * sin,
                cos + Math.Pow(zUnitVector.Z, 2) * (1 - cos),
                0,

                0, 0, 0, 1
            );
        }

    }
    #endregion

    #region Loads
    public partial class StructuralLoadCase
    {
        public StructuralLoadCase() { }

        public StructuralLoadCase(StructuralLoadCaseType caseType, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.CaseType = caseType;
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class StructuralLoadTask
    {
        public StructuralLoadTask() { }

        public StructuralLoadTask(StructuralLoadTaskType taskType, string[] loadCaseRefs, double[] loadFactors, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.TaskType = taskType;
            this.LoadCaseRefs = loadCaseRefs.ToList();
            this.LoadFactors = loadFactors.ToList();
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class StructuralLoadCombo
    {
        public StructuralLoadCombo() { }

        public StructuralLoadCombo(StructuralLoadComboType comboType, string[] loadTaskRefs, double[] loadTaskFactors, string[] loadComboRefs, double[] loadComboFactors, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.ComboType = comboType;
            this.LoadTaskRefs = loadTaskRefs.ToList();
            this.LoadTaskFactors = loadTaskFactors.ToList();
            this.LoadComboRefs = loadComboRefs.ToList();
            this.LoadComboFactors = loadComboFactors.ToList();
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class Structural0DLoad
    {
        public Structural0DLoad() { }

        public Structural0DLoad(StructuralVectorSix loading, string[] nodeRefs, string loadCaseRef, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Loading = loading;
            this.NodeRefs = nodeRefs == null ? null : nodeRefs.ToList();
            this.LoadCaseRef = loadCaseRef;
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class Structural1DLoad
    {
        public Structural1DLoad() { }

        public Structural1DLoad(StructuralVectorSix loading, string[] elementRefs, string loadCaseRef, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Loading = loading;
            this.ElementRefs = elementRefs == null ? null : elementRefs.ToList();
            this.LoadCaseRef = loadCaseRef;
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class Structural2DLoad
    {
        public Structural2DLoad() { }

        public Structural2DLoad(StructuralVectorThree loading, string[] elementRefs, string loadCaseRef, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Loading = loading;
            this.ElementRefs = elementRefs == null ? null : elementRefs.ToList();
            this.LoadCaseRef = loadCaseRef;
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class Structural2DLoadPanel
    {
        public Structural2DLoadPanel() { }

        public Structural2DLoadPanel(double[] value, StructuralVectorThree loading, string loadCaseRef, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Properties = properties;
            this.Value = value.ToList();
            this.Loading = loading;
            this.LoadCaseRef = loadCaseRef;
            this.StructuralId = structuralId;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            for (int i = 0; i < this.Value.Count(); i++)
                this.Value[i] *= factor;

            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }
    #endregion

    #region Properties
    public partial class StructuralMaterialConcrete
    {
        public StructuralMaterialConcrete() { }

        public StructuralMaterialConcrete(double youngsModulus, double shearModulus, double poissonsRatio, double density, double coeffThermalExpansion, double compressiveStrength, double maxStrain, double aggragateSize, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.YoungsModulus = youngsModulus;
            this.ShearModulus = shearModulus;
            this.PoissonsRatio = poissonsRatio;
            this.Density = density;
            this.CoeffThermalExpansion = coeffThermalExpansion;
            this.CompressiveStrength = compressiveStrength;
            this.MaxStrain = maxStrain;
            this.AggragateSize = aggragateSize;
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class StructuralMaterialSteel
    {
        public StructuralMaterialSteel() { }

        public StructuralMaterialSteel(double youngsModulus, double shearModulus, double poissonsRatio, double density, double coeffThermalExpansion, double yieldStrength, double ultimateStrength, double maxStrain, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.YoungsModulus = youngsModulus;
            this.ShearModulus = shearModulus;
            this.PoissonsRatio = poissonsRatio;
            this.Density = density;
            this.CoeffThermalExpansion = coeffThermalExpansion;
            this.YieldStrength = yieldStrength;
            this.UltimateStrength = ultimateStrength;
            this.MaxStrain = maxStrain;
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class Structural1DProperty
    {
        public Structural1DProperty() { }

        public Structural1DProperty(SpeckleObject profile, Structural1DPropertyShape shape, bool hollow, double thickness, string materialRef, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Profile = profile;
            this.Shape = shape;
            this.Hollow = hollow;
            this.Thickness = thickness;
            this.MaterialRef = materialRef;
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Profile.Scale(factor);
            this.Thickness *= factor;

            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class Structural2DProperty
    {
        public Structural2DProperty() { }

        public Structural2DProperty(double thickness, string materialRef, Structural2DPropertyReferenceSurface referenceSurface, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Thickness = thickness;
            this.MaterialRef = materialRef;
            this.ReferenceSurface = referenceSurface;
            this.StructuralId = structuralId;
            this.Properties = properties;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            this.Thickness *= factor;

            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }
    #endregion

    #region Nodes and Elements
    public partial class StructuralNode
    {
        public StructuralNode() { }

        public StructuralNode(double[] value, StructuralAxis axis, StructuralVectorBoolSix restraint, StructuralVectorSix stiffness, double mass, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Properties = properties;
            this.Value = value.ToList();
            this.Axis = axis;
            this.Restraint = restraint;
            this.Stiffness = stiffness;
            this.Mass = mass;
            this.StructuralId = structuralId;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            for (int i = 0; i < this.Value.Count(); i++)
                this.Value[i] *= factor;

            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class Structural1DElement
    {
        public Structural1DElement() { }

        public Structural1DElement(double[] value, Structural1DElementType elementType, string propertyRef, StructuralVectorThree zAxis, StructuralVectorBoolSix[] endRelease, StructuralVectorThree[] offset, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Properties = properties;
            this.Value = value.ToList();
            this.ElementType = elementType;
            this.PropertyRef = propertyRef;
            this.ZAxis = zAxis;
            this.EndRelease = endRelease == null ? null : EndRelease.ToList();
            this.Offset = offset == null ? null : offset.ToList();
            this.StructuralId = structuralId;

            GenerateHash();
        }

        public override void Scale(double factor)
        {
            for (int i = 0; i < this.Value.Count(); i++)
                this.Value[i] *= factor;

            if (Offset != null)
                for (int i = 0; i < this.Offset.Count(); i++)
                    this.Offset[i].Scale(factor);

            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class Structural1DElementPolyline
    {
        public Structural1DElementPolyline() { }

        public Structural1DElementPolyline(double[] value, Structural1DElementType elementType, string propertyRef, StructuralVectorThree[] zAxis, StructuralVectorBoolSix[] endRelease, StructuralVectorThree[] offset, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Properties = properties;
            this.Value = value.ToList();
            this.ElementType = elementType;
            this.PropertyRef = propertyRef;
            this.ZAxis = zAxis == null ? null : zAxis.ToList();
            this.EndRelease = endRelease == null ? null : endRelease.ToList();
            this.Offset = offset == null ? null : offset.ToList();
            this.StructuralId = structuralId;

            GenerateHash();
        }

        public Structural1DElementPolyline(Structural1DElement[] elements, string structuralId = null, Dictionary<string, object> properties = null)
        {
            if (elements.Length == 0)
                throw new Exception("No elements specified.");

            this.Properties = properties;
            this.Value = new List<double>(elements[0].Value.Take(3).ToArray());
            this.ElementType = elements[0].ElementType;
            this.PropertyRef = elements[0].PropertyRef;
            this.ZAxis = new List<StructuralVectorThree>();
            this.EndRelease = new List<StructuralVectorBoolSix>();
            this.Offset = new List<StructuralVectorThree>();

            foreach (Structural1DElement element in elements)
            {
                if (this.ElementType != element.ElementType)
                    throw new Exception("Different ElementTypes.");

                if (this.PropertyRef != element.PropertyRef)
                    throw new Exception("Different PropertyRef.");

                if (!this.Value.Skip(this.Value.Count() - 3).Take(3).SequenceEqual(element.Value.Take(3)))
                    throw new Exception("Elements not continuous.");

                this.Value.AddRange(element.Value.Skip(3).Take(3));
                this.ZAxis.Add(element.ZAxis);
                this.EndRelease.AddRange(element.EndRelease);
                this.Offset.AddRange(element.Offset);
            }

            this.StructuralId = structuralId;

            GenerateHash();
        }

        public Structural1DElement[] Explode()
        {
            List<Structural1DElement> elements = new List<Structural1DElement>();

            for (int i = 0; i < Value.Count() / 3 - 1; i++)
            {
                Structural1DElement element = new Structural1DElement(
                    Value.Skip(i * 3).Take(6).ToArray(),
                    ElementType,
                    PropertyRef,
                    ZAxis == null || ZAxis.Count() <= i ? null : ZAxis[i],
                    EndRelease == null || EndRelease.Count() >= i * 2 + 2 ? null : EndRelease.Skip(i * 2).Take(2).ToArray(),
                    Offset == null || Offset.Count() >= i * 2 + 2 ? null : Offset.Skip(i * 2).Take(2).ToArray()
                );
                element.Dummy = Dummy;
                element.MeshSize = MeshSize;
                elements.Add(element);
            }

            return elements.ToArray();
        }

        public override void Scale(double factor)
        {
            for (int i = 0; i < this.Value.Count(); i++)
                this.Value[i] *= factor;

            if (this.Offset != null)
                for (int i = 0; i < this.Offset.Count(); i++)
                    this.Offset[i].Scale(factor);

            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }
    }

    public partial class Structural2DElementMesh
    {
        public Structural2DElementMesh() { }

        public Structural2DElementMesh(double[] vertices, int[] faces, int[] colors, Structural2DElementType elementType, string propertyRef, StructuralAxis axis, double offset, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Properties = properties;
            this.Vertices = vertices.ToList();
            this.Faces = faces.ToList();
            this.Colors = colors == null ? null : colors.ToList();
            this.ElementType = elementType;
            this.PropertyRef = propertyRef;
            this.Axis = axis;
            this.Offset = offset;
            this.StructuralId = structuralId;

            this.TextureCoordinates = null;

            GenerateHash();
        }

        public Structural2DElementMesh(double[] edgeVertices, int? color, Structural2DElementType elementType, string propertyRef, StructuralAxis axis, double offset, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Properties = properties;
            this.Vertices = edgeVertices.ToList();

            // Perform mesh making
            List<List<int>> faces = SplitMesh(
                edgeVertices,
                (Enumerable.Range(0, edgeVertices.Count() / 3).ToArray()));

            this.Faces = new List<int>();

            foreach (List<int> face in faces)
            {
                this.Faces.Add(face.Count() - 3);
                this.Faces.AddRange(face);
            }

            if (color != null)
                this.Colors = Enumerable.Repeat(color.Value, this.Vertices.Count() / 3).ToList();
            else
                this.Colors = new List<int>();

            this.ElementType = elementType;
            this.PropertyRef = propertyRef;
            this.Axis = axis;
            this.Offset = offset;
            this.StructuralId = structuralId;

            this.TextureCoordinates = null;

            GenerateHash();
        }

        public Structural2DElementMesh[] Explode()
        {
            List<Structural2DElementMesh> elements = new List<Structural2DElementMesh>();

            for (int i = 0; i < Faces.Count(); i++)
            {
                List<double> vertices = new List<double>();
                List<int> colors = new List<int>();

                int numVertices = Faces[i++] + 3;
                for (int j = 0; j < numVertices; j++)
                {
                    if (Colors != null && Colors.Count() > Faces[i])
                    {
                        colors.Add(Colors[Faces[i]]);
                    }
                    vertices.AddRange(Vertices.Skip(Faces[i++] * 3).Take(3));
                }
                i--;

                Structural2DElementMesh element = new Structural2DElementMesh(
                    vertices.ToArray(),
                    (new List<int>() { numVertices - 3 }).Concat(Enumerable.Range(0, numVertices)).ToArray(),
                    colors.Count() == vertices.Count() / 3 ? colors.ToArray() : new int[0],
                    ElementType,
                    PropertyRef,
                    Axis,
                    Offset
                );
                element.Dummy = Dummy;
                element.MeshSize = MeshSize;
                elements.Add(element);
            }

            return elements.ToArray();
        }

        public List<int[]> Edges()
        {
            List<int[]> edgeConnectivities = new List<int[]>();

            // Get face connectivities and close loop
            List<int[]> faceConnnectivities = new List<int[]>();
            for (int i = 0; i < Faces.Count(); i++)
            {
                int numVertices = Faces[i] + 3;
                i++;
                faceConnnectivities.Add(Faces.Skip(i).Take(numVertices).Concat(Faces.Skip(i).Take(1)).ToArray());
                i += numVertices - 1;
            }

            // Get distinct edges
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

            foreach (int[] conn in faceConnnectivities)
            {
                for (int i = 0; i < conn.Length - 1; i++)
                {
                    if (edges.Any(e => (e.Item1 == conn[i] & e.Item2 == conn[i + 1]) |
                        (e.Item1 == conn[i + 1] & e.Item2 == conn[i])))
                    {
                        edges.Remove(new Tuple<int, int>(conn[i], conn[i + 1]));
                        edges.Remove(new Tuple<int, int>(conn[i + 1], conn[i]));
                    }
                    else
                    {
                        if (conn[i] < conn[i + 1])
                            edges.Add(new Tuple<int, int>(conn[i], conn[i + 1]));
                        else
                            edges.Add(new Tuple<int, int>(conn[i + 1], conn[i]));
                    }
                }
            }

            // Reorder the edges
            List<int> currentLoop = new List<int>();
            currentLoop.Add(edges[0].Item1);
            currentLoop.Add(edges[0].Item2);

            edges.RemoveAt(0);

            while (edges.Count > 0)
            {
                int commonVertex = currentLoop[currentLoop.Count() - 1];

                List<Tuple<int, int>> nextEdge = edges.Where(e => e.Item1 == commonVertex | e.Item2 == commonVertex).ToList();

                if (nextEdge.Count > 0)
                {
                    currentLoop.Add(nextEdge[0].Item1 == commonVertex ? nextEdge[0].Item2 : nextEdge[0].Item1);
                    edges.Remove(nextEdge[0]);
                }
                else
                {
                    // Next edge not found. Stop looking for more
                    break;
                }

                if (currentLoop[0] == currentLoop[currentLoop.Count() - 1])
                {
                    currentLoop.RemoveAt(0);

                    edgeConnectivities.Add(currentLoop.ToArray());
                    currentLoop = new List<int>();

                    if (edges.Count > 0)
                    {
                        currentLoop.Add(edges[0].Item1);
                        currentLoop.Add(edges[0].Item2);
                        edges.RemoveAt(0);
                    }
                }
            }

            return edgeConnectivities;
        }

        public override void Scale(double factor)
        {
            for (int i = 0; i < this.Vertices.Count(); i++)
                this.Vertices[i] *= factor;
            this.Offset *= factor;
            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }

        //TODO: These methods need to be disintegrated 
        #region Mesh Generation Helper Functions
        private static List<List<int>> SplitMesh(double[] coordinates, int[] mesh)
        {
            if (mesh.Length <= 4) return new List<List<int>>() { mesh.ToList() };

            // Need to ensure same area!
            double currArea = IntegrateHasher(coordinates, mesh);

            // Assume area doesn't twist on itself
            if (currArea < 0)
            {
                mesh = mesh.Reverse().ToArray();
                currArea *= -1;
            }

            int indexToCut = 0;
            int numCut = 3;
            double bestCost = currArea * 10; // TODO: figure out a better way
            List<int> newFace1 = new List<int>();
            List<int> newFace2 = new List<int>();

            do
            {
                List<int> face1 = mesh.Take(numCut).ToList();
                List<int> face2 = mesh.Skip(numCut - 1).ToList();
                face2.Add(mesh[0]);

                double cost1 = IntegrateHasher(coordinates, face1.ToArray());
                double cost2 = IntegrateHasher(coordinates, face2.ToArray());

                if (cost1 > 0 & cost2 > 0)
                {
                    // Check to make sure that the new region does not encompass the other's points
                    bool flag = false;
                    for (int i = 1; i < face2.Count() - 1; i++)
                    {
                        if (InTri(coordinates, face1.ToArray(), face2[i]))
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        double cost = Math.Abs(cost1 + cost2 - currArea);
                        if (bestCost > cost)
                        {
                            // Track best solution
                            bestCost = cost;
                            newFace1 = face1;
                            newFace2 = face2;
                        }
                    }
                }

                mesh = mesh.Skip(1).Take(mesh.Count() - 1).Concat(new int[] { mesh[0] }).ToArray();
                indexToCut++;

                if (indexToCut >= mesh.Count())
                    break;

            } while (bestCost > 1e-10);

            List<List<int>> returnVals = new List<List<int>>();
            if (newFace1.Count() > 0)
                returnVals.AddRange(SplitMesh(coordinates, newFace1.ToArray()));
            if (newFace2.Count() > 0)
                returnVals.AddRange(SplitMesh(coordinates, newFace2.ToArray()));
            return returnVals;
        }

        private static double IntegrateHasher(double[] coordinates, int[] vertices)
        {
            // Get coordinates
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> z = new List<double>();

            foreach (int e in vertices)
            {
                x.Add(coordinates[e * 3]);
                y.Add(coordinates[e * 3 + 1]);
                z.Add(coordinates[e * 3 + 2]);
            }

            // Close the loop
            x.Add(x[0]);
            y.Add(y[0]);
            z.Add(z[0]);

            //Integrate
            double area1 = 0;
            for (int i = 0; i < x.Count() - 1; i++)
                area1 += x[i] * y[i + 1] - y[i] * x[i + 1];

            if (Math.Abs(area1) > 1e-16) return area1;

            //Integrate
            double area2 = 0;
            for (int i = 0; i < x.Count() - 1; i++)
                area2 += x[i] * z[i + 1] - z[i] * y[i + 1];

            if (Math.Abs(area2) > 1e-16) return area2;

            //Integrate
            double area3 = 0;
            for (int i = 0; i < y.Count() - 1; i++)
                area3 += y[i] * z[i + 1] - z[i] * y[i + 1];

            if (Math.Abs(area3) > 1e-16) return area3;

            return 0;
        }

        public static bool InTri(double[] coordinates, int[] tri, int point)
        {
            // Get coordinates
            Point3D p0 = new Point3D(coordinates[tri[0] * 3], coordinates[tri[0] * 3 + 1], coordinates[tri[0] * 3 + 2]);
            Point3D p1 = new Point3D(coordinates[tri[1] * 3], coordinates[tri[1] * 3 + 1], coordinates[tri[1] * 3 + 2]);
            Point3D p2 = new Point3D(coordinates[tri[2] * 3], coordinates[tri[2] * 3 + 1], coordinates[tri[2] * 3 + 2]);
            Point3D p = new Point3D(coordinates[point * 3], coordinates[point * 3 + 1], coordinates[point * 3 + 2]);

            Vector3D u = Point3D.Subtract(p1, p0);
            Vector3D v = Point3D.Subtract(p2, p0);
            Vector3D n = Vector3D.CrossProduct(u, v);
            Vector3D w = Point3D.Subtract(p, p0);

            double gamma = Vector3D.DotProduct(Vector3D.CrossProduct(u, w), n) / (n.Length * n.Length);
            double beta = Vector3D.DotProduct(Vector3D.CrossProduct(w, v), n) / (n.Length * n.Length);
            double alpha = 1 - gamma - beta;

            if (alpha >= 0 & beta >= 0 & gamma >= 0 & alpha <= 1 & beta <= 1 & gamma <= 1)
                return true;
            else
                return false;
        }
        #endregion
    }

    public partial class Structural2DVoid
    {
        public Structural2DVoid() { }

        public Structural2DVoid(double[] vertices, int[] faces, int[] colors, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Properties = properties;
            this.Vertices = vertices.ToList();
            this.Faces = faces.ToList();
            this.Colors = colors == null ? null : colors.ToList();
            this.StructuralId = structuralId;

            this.TextureCoordinates = null;

            GenerateHash();
        }

        public Structural2DVoid(double[] edgeVertices, int? color, string structuralId = null, Dictionary<string, object> properties = null)
        {
            this.Properties = properties;
            this.Vertices = edgeVertices.ToList();

            // Perform mesh making
            List<List<int>> faces = SplitMesh(
                edgeVertices,
                (Enumerable.Range(0, edgeVertices.Count() / 3).ToArray()));

            this.Faces = new List<int>();

            foreach (List<int> face in faces)
            {
                this.Faces.Add(face.Count() - 3);
                this.Faces.AddRange(face);
            }

            if (color != null)
                this.Colors = Enumerable.Repeat(color.Value, this.Vertices.Count() / 3).ToList();
            else
                this.Colors = new List<int>();

            this.StructuralId = structuralId;

            this.TextureCoordinates = null;

            GenerateHash();
        }

        public List<int[]> Edges()
        {
            List<int[]> edgeConnectivities = new List<int[]>();

            // Get face connectivities and close loop
            List<int[]> faceConnnectivities = new List<int[]>();
            for (int i = 0; i < Faces.Count(); i++)
            {
                int numVertices = Faces[i] + 3;
                i++;
                faceConnnectivities.Add(Faces.Skip(i).Take(numVertices).Concat(Faces.Skip(i).Take(1)).ToArray());
                i += numVertices - 1;
            }

            // Get distinct edges
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();

            foreach (int[] conn in faceConnnectivities)
            {
                for (int i = 0; i < conn.Length - 1; i++)
                {
                    if (edges.Any(e => (e.Item1 == conn[i] & e.Item2 == conn[i + 1]) |
                        (e.Item1 == conn[i + 1] & e.Item2 == conn[i])))
                    {
                        edges.Remove(new Tuple<int, int>(conn[i], conn[i + 1]));
                        edges.Remove(new Tuple<int, int>(conn[i + 1], conn[i]));
                    }
                    else
                    {
                        if (conn[i] < conn[i + 1])
                            edges.Add(new Tuple<int, int>(conn[i], conn[i + 1]));
                        else
                            edges.Add(new Tuple<int, int>(conn[i + 1], conn[i]));
                    }
                }
            }

            // Reorder the edges
            List<int> currentLoop = new List<int>();
            currentLoop.Add(edges[0].Item1);
            currentLoop.Add(edges[0].Item2);

            edges.RemoveAt(0);

            while (edges.Count > 0)
            {
                int commonVertex = currentLoop[currentLoop.Count() - 1];

                List<Tuple<int, int>> nextEdge = edges.Where(e => e.Item1 == commonVertex | e.Item2 == commonVertex).ToList();

                if (nextEdge.Count > 0)
                {
                    currentLoop.Add(nextEdge[0].Item1 == commonVertex ? nextEdge[0].Item2 : nextEdge[0].Item1);
                    edges.Remove(nextEdge[0]);
                }
                else
                {
                    // Next edge not found. Stop looking for more
                    break;
                }

                if (currentLoop[0] == currentLoop[currentLoop.Count() - 1])
                {
                    currentLoop.RemoveAt(0);

                    edgeConnectivities.Add(currentLoop.ToArray());
                    currentLoop = new List<int>();

                    if (edges.Count > 0)
                    {
                        currentLoop.Add(edges[0].Item1);
                        currentLoop.Add(edges[0].Item2);
                        edges.RemoveAt(0);
                    }
                }
            }

            return edgeConnectivities;
        }

        public override void Scale(double factor)
        {
            for (int i = 0; i < this.Vertices.Count(); i++)
                this.Vertices[i] *= factor;

            this.Properties = ScaleProperties(this.Properties, factor);
            this.GenerateHash();
        }

        //TODO: These methods need to be disintegrated 
        #region Mesh Generation Helper Functions
        private static List<List<int>> SplitMesh(double[] coordinates, int[] mesh)
        {
            if (mesh.Length <= 4) return new List<List<int>>() { mesh.ToList() };

            // Need to ensure same area!
            double currArea = IntegrateHasher(coordinates, mesh);

            // Assume area doesn't twist on itself
            if (currArea < 0)
            {
                mesh = mesh.Reverse().ToArray();
                currArea *= -1;
            }

            int indexToCut = 0;
            int numCut = 3;
            double bestCost = currArea * 10; // TODO: figure out a better way
            List<int> newFace1 = new List<int>();
            List<int> newFace2 = new List<int>();

            do
            {
                List<int> face1 = mesh.Take(numCut).ToList();
                List<int> face2 = mesh.Skip(numCut - 1).ToList();
                face2.Add(mesh[0]);

                double cost1 = IntegrateHasher(coordinates, face1.ToArray());
                double cost2 = IntegrateHasher(coordinates, face2.ToArray());

                if (cost1 > 0 & cost2 > 0)
                {
                    // Check to make sure that the new region does not encompass the other's points
                    bool flag = false;
                    for (int i = 1; i < face2.Count() - 1; i++)
                    {
                        if (InTri(coordinates, face1.ToArray(), face2[i]))
                        {
                            flag = true;
                            break;
                        }
                    }

                    if (!flag)
                    {
                        double cost = Math.Abs(cost1 + cost2 - currArea);
                        if (bestCost > cost)
                        {
                            // Track best solution
                            bestCost = cost;
                            newFace1 = face1;
                            newFace2 = face2;
                        }
                    }
                }

                mesh = mesh.Skip(1).Take(mesh.Count() - 1).Concat(new int[] { mesh[0] }).ToArray();
                indexToCut++;

                if (indexToCut >= mesh.Count())
                    break;

            } while (bestCost > 1e-10);

            List<List<int>> returnVals = new List<List<int>>();
            if (newFace1.Count() > 0)
                returnVals.AddRange(SplitMesh(coordinates, newFace1.ToArray()));
            if (newFace2.Count() > 0)
                returnVals.AddRange(SplitMesh(coordinates, newFace2.ToArray()));
            return returnVals;
        }

        private static double IntegrateHasher(double[] coordinates, int[] vertices)
        {
            // Get coordinates
            List<double> x = new List<double>();
            List<double> y = new List<double>();
            List<double> z = new List<double>();

            foreach (int e in vertices)
            {
                x.Add(coordinates[e * 3]);
                y.Add(coordinates[e * 3 + 1]);
                z.Add(coordinates[e * 3 + 2]);
            }

            // Close the loop
            x.Add(x[0]);
            y.Add(y[0]);
            z.Add(z[0]);

            //Integrate
            double area1 = 0;
            for (int i = 0; i < x.Count() - 1; i++)
                area1 += x[i] * y[i + 1] - y[i] * x[i + 1];

            if (Math.Abs(area1) > 1e-16) return area1;

            //Integrate
            double area2 = 0;
            for (int i = 0; i < x.Count() - 1; i++)
                area2 += x[i] * z[i + 1] - z[i] * y[i + 1];

            if (Math.Abs(area2) > 1e-16) return area2;

            //Integrate
            double area3 = 0;
            for (int i = 0; i < y.Count() - 1; i++)
                area3 += y[i] * z[i + 1] - z[i] * y[i + 1];

            if (Math.Abs(area3) > 1e-16) return area3;

            return 0;
        }

        public static bool InTri(double[] coordinates, int[] tri, int point)
        {
            // Get coordinates
            Point3D p0 = new Point3D(coordinates[tri[0] * 3], coordinates[tri[0] * 3 + 1], coordinates[tri[0] * 3 + 2]);
            Point3D p1 = new Point3D(coordinates[tri[1] * 3], coordinates[tri[1] * 3 + 1], coordinates[tri[1] * 3 + 2]);
            Point3D p2 = new Point3D(coordinates[tri[2] * 3], coordinates[tri[2] * 3 + 1], coordinates[tri[2] * 3 + 2]);
            Point3D p = new Point3D(coordinates[point * 3], coordinates[point * 3 + 1], coordinates[point * 3 + 2]);

            Vector3D u = Point3D.Subtract(p1, p0);
            Vector3D v = Point3D.Subtract(p2, p0);
            Vector3D n = Vector3D.CrossProduct(u, v);
            Vector3D w = Point3D.Subtract(p, p0);

            double gamma = Vector3D.DotProduct(Vector3D.CrossProduct(u, w), n) / (n.Length * n.Length);
            double beta = Vector3D.DotProduct(Vector3D.CrossProduct(w, v), n) / (n.Length * n.Length);
            double alpha = 1 - gamma - beta;

            if (alpha >= 0 & beta >= 0 & gamma >= 0 & alpha <= 1 & beta <= 1 & gamma <= 1)
                return true;
            else
                return false;
        }
        #endregion
    }
    #endregion
}