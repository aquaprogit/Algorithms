using VertexABC.Hive;

namespace VertexABC;

class ABC
{
    private readonly int[][] _graph;

    private int[] _bestSolution;
    private double _bestFitness;
    private readonly int _numBees;
    private readonly int _numOnlookers;
    private readonly int _numScouts;
    private readonly int _lowerBound;
    private readonly int _upperBound;
    private readonly int _totalEdges;
    private const int MAX_ITERATIONS = 10000;
    public ABC(int[][] graph, int numBees, int numOnlookers, int numScouts, int lowerBound, int upperBound)
    {
        _graph = graph;
        Solution.Graph = graph;
        _totalEdges = graph.Select(x => x.Length).Sum();
        Solution.TotalEdges = _totalEdges;
        _numBees = numBees;
        _numOnlookers = numOnlookers;
        _numScouts = numScouts;
        _lowerBound = lowerBound;
        _upperBound = upperBound;
        _bestSolution = InitializePopulation();
        _bestFitness = Fitness(_bestSolution);
    }

    public Solution Solve(bool printIterations = false)
    {
        for (int i = 0; i < MAX_ITERATIONS; i++)
        {
            List<ScoutBee> scoutBees = new List<ScoutBee>();
            List<Bee> otherBees = new List<Bee>();

            for (int j = 0; j < _numBees; j++)
                otherBees.Add(new EmployedBee(_lowerBound, _upperBound));

            for (int j = 0; j < _numOnlookers; j++)
                otherBees.Add(new OnlookerBee(_lowerBound, _upperBound));

            for (int j = 0; j < _numScouts; j++)
                scoutBees.Add(new ScoutBee());

            HashSet<Solution> newSolutions = new HashSet<Solution>();
            foreach (Bee bee in otherBees)
            {
                if (bee is ScoutBee)
                    continue;
                newSolutions.Add(new Solution(bee.GenerateSolution(_graph, _bestSolution)));
            }
            foreach (ScoutBee bee in scoutBees)
            {
                newSolutions.Add(new Solution(bee.GenerateSolution(_graph)));
            }

            Solution bestSolution = newSolutions.OrderBy(sol => sol.Fitness)
                                                .OrderBy(solution => solution.ColorSet.Length)
                                                .First()!;

            _bestSolution = bestSolution.ColorSet;
            _bestFitness = bestSolution.Fitness;

            if (printIterations && i % (MAX_ITERATIONS / 100) == 0)
            {
                Console.WriteLine($"Iteration: {i}, Fitness: {Math.Round(_bestFitness, 5)}, UsedColors: {_bestSolution.Distinct().Count()}");
            }
        }
        return new Solution(_bestSolution);
    }
    private int[] InitializePopulation()
    {
        int[] initialSolution = new ScoutBee().GenerateSolution(_graph);
        return initialSolution;
    }
    private double Fitness(int[] solution)
    {
        int violations = 0;
        for (int i = 0; i < _graph.Length; i++)
        {
            for (int j = 0; j < _graph[i].Length; j++)
            {
                if (solution[i] == solution[_graph[i][j]])
                    violations++;
            }
        }
        return (double)violations / _totalEdges;
    }
}
