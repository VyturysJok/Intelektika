using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelektikaProj
{
    public class Mushroom
    {
        public bool IsEdible { get; set; }

        public CapShape CapShapeValue { get; set; }
        public CapSurface CapSurfaceValue { get; set; }
        public CapColor CapColorValue { get; set; }

        public Bruises BruisesValue { get; set; }
        public Odor OdorValue { get; set; }

        public GillAttachment GillAttachmentValue { get; set; }
        public GillSpacing GillSpacingValue { get; set; }
        public GillSize GillSizeValue { get; set; }
        public GillColor GillColorValue { get; set; }

        public StalkShape StalkShapeValue { get; set; }
        public StalkRoot StalkRootValue { get; set; }
        public StalkSurfaceAboveRing StalkSurfaceAboveRingValue { get; set; }
        public StalkSurfaceBelowRing StalkSurfaceBelowRingValue { get; set; }
        public StalkColorAboveRing StalkColorAboveRingValue { get; set; }
        public StalkColorBelowRing StalkColorBelowRingValue { get; set; }

        public VeilType VeilTypeValue { get; set; }
        public VeilColor VeilColorValue { get; set; }

        public RingNumber RingNumberValue { get; set; }
        public RingType RingTypeValue { get; set; }

        public SporePrintColor SporePrintColorValue { get; set; }
        public Population PopulationValue { get; set; }
        public Habitat HabitatValue { get; set; }


        public Mushroom(string values)
        {
            ParseStringToVariables(values);
        }

        public Enum[] getAttributes()
        {
            return new Enum[]
            {
                CapShapeValue,
                CapSurfaceValue,
                CapColorValue,
                BruisesValue,
                OdorValue,
                GillAttachmentValue,
                GillSpacingValue,
                GillSizeValue,
                GillColorValue,
                StalkShapeValue,
                StalkRootValue,
                StalkSurfaceAboveRingValue,
                StalkSurfaceBelowRingValue,
                StalkColorAboveRingValue,
                StalkColorBelowRingValue,
                VeilTypeValue,
                VeilColorValue,
                RingNumberValue,
                RingTypeValue,
                SporePrintColorValue,
                PopulationValue,
                HabitatValue
            };
        }

        public void ParseStringToVariables(string values)
        {
            var parsedValues = values.Replace(",", string.Empty);

            IsEdible = parsedValues[0] == 'e';

            CapShapeValue = (CapShape) parsedValues[1];
            CapSurfaceValue = (CapSurface) parsedValues[2];
            CapColorValue = (CapColor) parsedValues[3];

            BruisesValue = (Bruises) parsedValues[4];
            OdorValue = (Odor) parsedValues[5];

            GillAttachmentValue = (GillAttachment) parsedValues[6];
            GillSpacingValue = (GillSpacing) parsedValues[7];
            GillSizeValue = (GillSize) parsedValues[8];
            GillColorValue = (GillColor) parsedValues[9];

            StalkShapeValue = (StalkShape) parsedValues[10];
            StalkRootValue = (StalkRoot) parsedValues[11];
            StalkSurfaceAboveRingValue = (StalkSurfaceAboveRing) parsedValues[12];
            StalkSurfaceBelowRingValue = (StalkSurfaceBelowRing) parsedValues[13];
            StalkColorAboveRingValue = (StalkColorAboveRing) parsedValues[14];
            StalkColorBelowRingValue = (StalkColorBelowRing) parsedValues[15];

            VeilTypeValue = (VeilType) parsedValues[16];
            VeilColorValue = (VeilColor) parsedValues[17];

            RingNumberValue = (RingNumber) parsedValues[18];
            RingTypeValue = (RingType) parsedValues[19];

            SporePrintColorValue = (SporePrintColor) parsedValues[20];
            PopulationValue = (Population) parsedValues[21];
            HabitatValue = (Habitat) parsedValues[22];

        }

        public enum CapShape  
        {
            bell = 'b',
            conical = 'c',
            convex = 'x',
            flat = 'f',
            knobbed = 'k',
            sunken = 's'
        }

        public enum CapSurface
        {
            fibrous = 'f',
            grooves = 'g',
            scaly = 'y',
            smooth = 's'
        }

        public enum CapColor
        {
            brown = 'n',
            buff = 'b',
            cinnamon = 'c',
            gray = 'g',
            green = 'r',
            pink = 'p',
            purple = 'u',
            red = 'e',
            white = 'w',
            yellow = 'y'
        }

        public enum Bruises
        {
            bruises = 't',
            no = 'f'
        }

        public enum Odor
        {
            almond = 'a',
            anise = 'l',
            creosote = 'c',
            fishy = 'y',
            foul = 'f',
            musty = 'm',
            none = 'n',
            pungent = 'p',
            spicy = 's'
        }

        public enum GillAttachment
        {
            attached = 'a',
            descending = 'd',
            free = 'f',
            notched = 'n'
        }

        public enum GillSpacing
        {
            close = 'c',
            crowded = 'w',
            distant = 'd'
        }

        public enum GillSize
        {
            broad = 'b',
            narrow = 'n'
        }

        public enum GillColor
        {
            black = 'k',
            brown = 'n',
            buff = 'b',
            chocolate = 'h',
            gray = 'g',
            green = 'r',
            orange = 'o',
            pink = 'p',
            purple = 'u',
            red = 'e',
            white = 'w',
            yellow = 'y'
        }

        public enum StalkShape
        {
            enlarging = 'e',
            tapering = 't'
        }

        public enum StalkRoot
        {
            bulbous = 'b',
            club = 'c',
            cup = 'u',
            equal = 'e',
            rhizomorphs = 'z',
            rooted = 'r',
            missing = '?'
        }

        public enum StalkSurfaceAboveRing
        {
            fibrous = 'f',
            scaly = 'y',
            silky = 'k',
            smooth = 's'
        }

        public enum StalkSurfaceBelowRing
        {
            fibrous = 'f',
            scaly = 'y',
            silky = 'k',
            smooth = 's'
        }

        public enum StalkColorAboveRing
        {
            brown = 'n',
            buff = 'b',
            cinnamon = 'c',
            gray = 'g',
            orange = 'o',
            pink = 'p',
            red = 'e',
            white = 'w',
            yellow = 'y'
        }

        public enum StalkColorBelowRing
        {
            brown = 'n',
            buff = 'b',
            cinnamon = 'c',
            gray = 'g',
            orange = 'o',
            pink = 'p',
            red = 'e',
            white = 'w',
            yellow = 'y'
        }

        public enum VeilType
        {
            partial = 'p',
            universal = 'u'
        }

        public enum VeilColor
        {
            brown = 'n',
            orange = 'o',
            white = 'w',
            yellow = 'y'
        }

        public enum RingNumber
        {
            none = 'n',
            one = 'o',
            two = 't'
        }

        public enum RingType
        {
            cobwebby = 'c',
            evanescent = 'e',
            flaring = 'f',
            large = 'l',
            none = 'n',
            pendant = 'p',
            sheathing = 's',
            zone = 'z'
        }

        public enum SporePrintColor
        {
            black = 'k',
            brown = 'n',
            buff = 'b',
            chocolate = 'h',
            green = 'r',
            orange = 'o',
            purple = 'u',
            white = 'w',
            yellow = 'y'
        }

        public enum Population
        {
            abundant = 'a',
            clustered = 'c',
            numerous = 'n',
            scattered = 's',
            several = 'v',
            solitary = 'y'
        }

        public enum Habitat
        {
            grasses = 'g',
            leaves = 'l',
            meadows = 'm',
            paths = 'p',
            urban = 'u',
            waste = 'w',
            woods = 'd'
        }
    }
}
