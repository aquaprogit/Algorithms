namespace AntsTSP;

internal class AntsSettings
{
    public static int VerticesCount { get; set; }
    public static int CommonAnts { get; set; }
    public static int EliteAnts { get; set; }
    public static double Alfa { get; set; }
    public static double Beta { get; set; }
    public static double Ro { get; set; }

    public static void ApplyParameter(Parameter param, double value)
    {
        switch (param)
        {
            case Parameter.Alfa:
                Alfa = value;
                break;
            case Parameter.Beta:
                Beta = value;
                break;
            case Parameter.Ro:
                Ro = value;
                break;
            case Parameter.CommonAnts:
                CommonAnts = (int)value;
                break;
            case Parameter.EliteAnts:
                EliteAnts = (int)value;
                break;
        }
    }

}