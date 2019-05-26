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
        public static List<Enum> GetCapShapeEnums()
        {
            return new List<Enum>
            {
                CapShape.bell,
                CapShape.conical,
                CapShape.convex,
                CapShape.flat,
                CapShape.knobbed,
                CapShape.sunken
            };
        }

        public enum CapSurface
        {
            fibrous = 'f',
            grooves = 'g',
            scaly = 'y',
            smooth = 's'
        }
        public static List<Enum> GetCapSurfaceEnums()
        {
            return  new List<Enum>
            {
                CapSurface.fibrous,
                CapSurface.grooves,
                CapSurface.scaly,
                CapSurface.smooth
            };
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
        public static List<Enum> GetCapColorEnums()
        {
            return new List<Enum>
            {
                CapColor.brown,
                CapColor.buff,
                CapColor.cinnamon,
                CapColor.gray,
                CapColor.green,
                CapColor.pink,
                CapColor.purple,
                CapColor.red,
                CapColor.white,
                CapColor.yellow
            };
        }

        public enum Bruises
        {
            bruises = 't',
            no = 'f'
        }
        public static List<Enum> GetBruisesEnums()
        {
            return new List<Enum>
            {
                Bruises.no,
                Bruises.bruises
            };
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
        public static List<Enum> GetOdorEnums()
        {
            return new List<Enum>
            {
                Odor.almond,
                Odor.anise,
                Odor.creosote,
                Odor.fishy,
                Odor.foul,
                Odor.musty,
                Odor.none,
                Odor.pungent,
                Odor.spicy
            };
        }

        public enum GillAttachment
        {
            attached = 'a',
            descending = 'd',
            free = 'f',
            notched = 'n'
        }
        public static List<Enum> GetGillAttachmentEnums()
        {
            return new List<Enum>
            {
                GillAttachment.attached,
                GillAttachment.descending,
                GillAttachment.free,
                GillAttachment.notched
            };
        }

        public enum GillSpacing
        {
            close = 'c',
            crowded = 'w',
            distant = 'd'
        }
        public static List<Enum> GetGillSpacingEnums()
        {
            return new List<Enum>
            {
                GillSpacing.close,
                GillSpacing.crowded,
                GillSpacing.distant
            };
        }

        public enum GillSize
        {
            broad = 'b',
            narrow = 'n'
        }
        public static List<Enum> GetGillSizeEnums()
        {
            return new List<Enum>
            {
                GillSize.broad,
                GillSize.narrow
            };
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
        public static List<Enum> GetGillColorEnums()
        {
            return new List<Enum>
            {
                GillColor.black,
                GillColor.brown,
                GillColor.buff,
                GillColor.chocolate,
                GillColor.green,
                GillColor.green,
                GillColor.orange,
                GillColor.pink,
                GillColor.purple,
                GillColor.red,
                GillColor.white,
                GillColor.yellow
            };
        }

        public enum StalkShape
        {
            enlarging = 'e',
            tapering = 't'
        }
        public static List<Enum> GetStalkShapeEnums()
        {
            return new List<Enum>
            {
                StalkShape.enlarging,
                StalkShape.tapering
            };
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
        public static List<Enum> GetStalkRootEnums()
        {
            return new List<Enum>
            {
                StalkRoot.bulbous,
                StalkRoot.club,
                StalkRoot.cup,
                StalkRoot.equal,
                StalkRoot.rhizomorphs,
                StalkRoot.rooted,
                StalkRoot.missing
            };
        }

        public enum StalkSurfaceAboveRing
        {
            fibrous = 'f',
            scaly = 'y',
            silky = 'k',
            smooth = 's'
        }
        public static List<Enum> GetStalkSurfaceAboveRingEnums()
        {
            return new List<Enum>
            {
                StalkSurfaceAboveRing.fibrous,
                StalkSurfaceAboveRing.scaly,
                StalkSurfaceAboveRing.silky,
                StalkSurfaceAboveRing.smooth
            };
        }

        public enum StalkSurfaceBelowRing
        {
            fibrous = 'f',
            scaly = 'y',
            silky = 'k',
            smooth = 's'
        }
        public static List<Enum> GetStalkSurfaceBelowRingEnums()
        {
            return new List<Enum>
            {
                StalkSurfaceBelowRing.fibrous,
                StalkSurfaceBelowRing.scaly,
                StalkSurfaceBelowRing.silky,
                StalkSurfaceBelowRing.smooth
            };
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
        public static List<Enum> StalkColorAboveRingEnums()
        {
            return new List<Enum>
            {
                StalkColorAboveRing.brown,
                StalkColorAboveRing.buff,
                StalkColorAboveRing.cinnamon,
                StalkColorAboveRing.gray,
                StalkColorAboveRing.orange,
                StalkColorAboveRing.pink,
                StalkColorAboveRing.red,
                StalkColorAboveRing.white,
                StalkColorAboveRing.yellow
            };
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
        public static List<Enum> GetStalkColorBelowRingEnums()
        {
            return new List<Enum>
            {
                StalkColorBelowRing.brown,
                StalkColorBelowRing.buff,
                StalkColorBelowRing.cinnamon,
                StalkColorBelowRing.gray,
                StalkColorBelowRing.orange,
                StalkColorBelowRing.pink,
                StalkColorBelowRing.red,
                StalkColorBelowRing.white,
                StalkColorBelowRing.yellow
            };
        }

        public enum VeilType
        {
            partial = 'p',
            universal = 'u'
        }
        public static List<Enum> GetVeilTypeEnums()
        {
            return new List<Enum>
            {
                VeilType.partial,
                VeilType.universal
            };
        }

        public enum VeilColor
        {
            brown = 'n',
            orange = 'o',
            white = 'w',
            yellow = 'y'
        }
        public static List<Enum> GetVeilColorEnums()
        {
            return new List<Enum>
            {
                VeilColor.brown,
                VeilColor.orange,
                VeilColor.white,
                VeilColor.yellow
            };
        }

        public enum RingNumber
        {
            none = 'n',
            one = 'o',
            two = 't'
        }
        public static List<Enum> GetRingNumberEnums()
        {
            return new List<Enum>
            {
                RingNumber.none,
                RingNumber.one,
                RingNumber.two
            };
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
        public static List<Enum> GetRingTypeEnums()
        {
            return new List<Enum>
            {
                RingType.cobwebby,
                RingType.evanescent,
                RingType.flaring,
                RingType.large,
                RingType.none,
                RingType.pendant,
                RingType.sheathing,
                RingType.zone
            };
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
        public static List<Enum> GetSporePrintColorEnums()
        {
            return new List<Enum>
            {
                SporePrintColor.black,
                SporePrintColor.brown,
                SporePrintColor.buff,
                SporePrintColor.chocolate,
                SporePrintColor.green,
                SporePrintColor.orange,
                SporePrintColor.purple,
                SporePrintColor.white,
                SporePrintColor.yellow
            };
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
        public static List<Enum> GetPopulationEnums()
        {
            return new List<Enum>
            {
                Population.abundant,
                Population.clustered,
                Population.numerous,
                Population.scattered,
                Population.several,
                Population.solitary
            };
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
        public static List<Enum> GetHabitatEnums()
        {
            return new List<Enum>
            {
                Habitat.grasses,
                Habitat.leaves,
                Habitat.meadows,
                Habitat.paths,
                Habitat.urban,
                Habitat.waste,
                Habitat.woods
            };
        }
    }
}
