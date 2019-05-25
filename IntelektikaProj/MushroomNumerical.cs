using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelektikaProj
{
    class MushroomNumerical
    {
        public bool IsEdible { get; set; }

        public double CapShapeValue { get; set; }
        public double CapSurfaceValue { get; set; }
        public double CapColorValue { get; set; }

        public double BruisesValue { get; set; }
        public double OdorValue { get; set; }

        public double GillAttachmentValue { get; set; }
        public double GillSpacingValue { get; set; }
        public double GillSizeValue { get; set; }
        public double GillColorValue { get; set; }

        public double StalkShapeValue { get; set; }
        public double StalkRootValue { get; set; }
        public double StalkSurfaceAboveRingValue { get; set; }
        public double StalkSurfaceBelowRingValue { get; set; }
        public double StalkColorAboveRingValue { get; set; }
        public double StalkColorBelowRingValue { get; set; }

        public double VeilTypeValue { get; set; }
        public double VeilColorValue { get; set; }

        public double RingNumberValue { get; set; }
        public double RingTypeValue { get; set; }

        public double SporePrintColorValue { get; set; }
        public double PopulationValue { get; set; }
        public double HabitatValue { get; set; }

        public MushroomNumerical(Mushroom mushroom)
        {
            MakeNumerical(mushroom);
        }

        public MushroomNumerical(bool IsEdible, double? CapShapeValue, double? CapSurfaceValue, double? CapColorValue, double? BruisesValue,
            double? OdorValue, double? GillAttachmentValue, double? GillSpacingValue, double? GillSizeValue, 
            double? GillColorValue, double? StalkShapeValue, double? StalkRootValue, double? StalkSurfaceAboveRingValue,
            double? StalkSurfaceBelowRingValue, double? StalkColorAboveRingValue, double? StalkColorBelowRingValue,
            double? VeilTypeValue, double? VeilColorValue, double? RingNumberValue, double? RingTypeValue, 
            double? SporePrintColorValue, double? PopulationValue, double? HabitatValue)
        {
            this.IsEdible = IsEdible;
            this.CapShapeValue = CapShapeValue != null ? (double)CapShapeValue : 0;
            this.CapSurfaceValue = CapSurfaceValue != null ? (double)CapSurfaceValue : 0;
            this.CapColorValue = CapColorValue != null ? (double)CapColorValue : 0;
            this.BruisesValue = BruisesValue != null ? (double)BruisesValue : 0;
            this.OdorValue = OdorValue != null ? (double)OdorValue : 0;
            this.GillAttachmentValue = GillAttachmentValue != null ? (double)GillAttachmentValue : 0;
            this.GillSpacingValue = GillSpacingValue != null ? (double)GillSpacingValue : 0;
            this.GillSizeValue = GillSizeValue != null ? (double)GillSizeValue : 0;
            this.GillColorValue = GillColorValue != null ? (double)GillColorValue : 0;
            this.StalkShapeValue = StalkShapeValue != null ? (double)StalkShapeValue : 0;
            this.StalkRootValue = StalkRootValue != null ? (double)StalkRootValue : 0;
            this.StalkSurfaceAboveRingValue = StalkSurfaceAboveRingValue != null ? (double)StalkSurfaceAboveRingValue : 0;
            this.StalkSurfaceBelowRingValue = StalkSurfaceBelowRingValue != null ? (double)StalkSurfaceBelowRingValue : 0;
            this.StalkColorAboveRingValue = StalkColorAboveRingValue != null ? (double)StalkColorAboveRingValue : 0;
            this.StalkColorBelowRingValue = StalkColorBelowRingValue != null ? (double)StalkColorBelowRingValue : 0;
            this.VeilTypeValue = VeilTypeValue != null ? (double)VeilTypeValue : 0;
            this.VeilColorValue = VeilColorValue != null ? (double)VeilColorValue : 0;
            this.RingNumberValue = RingNumberValue != null ? (double)RingNumberValue : 0;
            this.RingTypeValue = RingTypeValue != null ? (double)RingTypeValue : 0;
            this.SporePrintColorValue = SporePrintColorValue != null ? (double)SporePrintColorValue : 0;
            this.PopulationValue = PopulationValue != null ? (double)PopulationValue : 0;
            this.HabitatValue = HabitatValue != null ? (double)HabitatValue : 0;
        }

        private void MakeNumerical(Mushroom mushroom)
        {
            IsEdible = mushroom.IsEdible;
            CapShapeValue = (int)mushroom.CapShapeValue;
            CapSurfaceValue = (int)mushroom.CapSurfaceValue;
            CapColorValue = (int)mushroom.CapColorValue;

            BruisesValue = (int)mushroom.BruisesValue;
            OdorValue = (int)mushroom.OdorValue;

            GillAttachmentValue = (int)mushroom.GillAttachmentValue;
            GillSpacingValue = (int)mushroom.GillSpacingValue;
            GillSizeValue = (int)mushroom.GillSizeValue;
            GillColorValue = (int)mushroom.GillColorValue;

            StalkShapeValue = (int)mushroom.StalkShapeValue;
            StalkRootValue = (int)mushroom.StalkRootValue;
            StalkSurfaceAboveRingValue = (int)mushroom.StalkSurfaceAboveRingValue;
            StalkSurfaceBelowRingValue = (int)mushroom.StalkSurfaceBelowRingValue;
            StalkColorAboveRingValue = (int)mushroom.StalkColorAboveRingValue;
            StalkColorBelowRingValue = (int)mushroom.StalkColorBelowRingValue;

            VeilTypeValue = (int)mushroom.VeilTypeValue;
            VeilColorValue = (int)mushroom.VeilColorValue;

            RingNumberValue = (int)mushroom.RingNumberValue;
            RingTypeValue = (int)mushroom.RingTypeValue;

            SporePrintColorValue = (int)mushroom.SporePrintColorValue;
            PopulationValue = (int)mushroom.PopulationValue;
            HabitatValue = (int)mushroom.HabitatValue;
        }
    }
}
