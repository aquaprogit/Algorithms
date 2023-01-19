public class SolutionInfo
{
    public List<int> Cycle { get; set; }
    public int CycleLength { get; set; }
    public int Iterations { get; set; }
    public int Seconds { get; set; }
    public int CommonAnts { get; set; }
    public int EliteAnts { get; set; }
    public double Alfa { get; set; }
    public double Beta { get; set; }
    public double Ro { get; set; }

    public SolutionInfo(List<int> cycle, int cycleLength, int iterations, int seconds, int commonAnts, int eliteAnts, double alfa, double beta, double ro)
    {
        Cycle = cycle ?? throw new ArgumentNullException(nameof(cycle));
        CycleLength = cycleLength;
        Iterations = iterations;
        Seconds = seconds;
        CommonAnts = commonAnts;
        EliteAnts = eliteAnts;
        Alfa = alfa;
        Beta = beta;
        Ro = ro;
    }

    public double Get(Parameter param)
    {
        switch (param)
        {
            case Parameter.CommonAnts:
                return CommonAnts;
            case Parameter.EliteAnts:
                return EliteAnts;
            case Parameter.Alfa:
                return Alfa;
            case Parameter.Beta:
                return Beta;
            case Parameter.Ro:
                return Ro;
            default:
                throw new InvalidOperationException();
        }
    }
}