namespace VertexABC;

internal class ABC
{

    // Constants
    private const int MAX_ITERATIONS = 10_000;
    
    private int LowerBound { get; init; }
    private int UpperBound { get; init; }

    public int EmployedCount { get; init; }
    public int OnlookersCount { get; init; }
    public int ScoutsCount { get; init; }

    private const int NUM_BEES = 35;
    private const int NUM_ONLOOKERS = 5;
    private const int NUM_SCOUTS = 10;
    private const int LOWER_BOUND = 1;
    private const int UPPER_BOUND = 3;

    // Fields
    private readonly int _numColors;
    private readonly int[][] _graph;
    private readonly Random _rand;
    private int[] _bestSolution;
    private double _bestFitness;
    private double[][] _population;
    private readonly double[] _fitnessValues;
    private readonly double[] _trialValues;

    // Constructor
    public ABC(int[][] graph, int numColors)
    {
        _graph = graph;
        _numColors = numColors;
        _rand = new Random();
        _population = new double[NUM_BEES][];
        _fitnessValues = new double[NUM_BEES];
        for (int i = 0; i < NUM_BEES; i++)
        {
            _population[i] = InitializeBee();
            _fitnessValues[i] = Fitness(Array.ConvertAll(_population[i], x => (int)x));
        }
        _bestSolution = GetBestSolution();
        _bestFitness = Fitness(_bestSolution);
        _trialValues = new double[NUM_BEES];
        Array.Copy(_fitnessValues, _trialValues, NUM_BEES);

    }

    // Public methods
    public int[] Solve()
    {
        for (int i = 0; i < MAX_ITERATIONS; i++)
        {
            _population = SendEmployedBees(_population);
            _population = SendOnlookerBees(_population);
            _population = SendScoutBees(_population);
            int[] solution = GetBestSolution();
            double fitness = Fitness(solution);
            if (fitness < _bestFitness)
            {
                _bestSolution = solution;
                _bestFitness = fitness;
            }
        }
        return _bestSolution;
    }

    // Private methods
    private double[] InitializeBee()
    {
        double[] bee = new double[_graph.Length];
        for (int i = 0; i < _graph.Length; i++)
        {
            bee[i] = _rand.NextDouble() * (_numColors - 1) + 1;
        }
        return bee;
    }

    private double[] GenerateNeighbor(double[] bee)
    {
        double[] neighbor = (double[])bee.Clone();
        int dimension = _rand.Next(_graph.Length);
        neighbor[dimension] = neighbor[dimension] + (_rand.NextDouble() - 0.5) * 2 * (UPPER_BOUND - LOWER_BOUND);
        if (neighbor[dimension] < LOWER_BOUND)
        {
            neighbor[dimension] = LOWER_BOUND;
        }
        else if (neighbor[dimension] > UPPER_BOUND)
        {
            neighbor[dimension] = UPPER_BOUND;
        }
        return neighbor;
    }

    private double[][] SendEmployedBees(double[][] population)
    {
        for (int i = 0; i < population.Length; i++)
        {
            double[] neighbor = GenerateNeighbor(population[i]);
            int[] intNeighbor = Array.ConvertAll(neighbor, x => (int)x);
            double fitness = Fitness(intNeighbor);
            if (fitness < _fitnessValues[i])
            {
                population[i] = neighbor;
                _fitnessValues[i] = fitness;
                _trialValues[i] = 0;
            }
            else
            {
                _trialValues[i]++;
            }
        }
        return population;
    }

    private double[][] SendOnlookerBees(double[][] population)
    {
        int[] prob = CalculateProbabilities();
        for (int i = 0; i < NUM_ONLOOKERS; i++)
        {
            int index = SelectNeighbor(prob);
            double[] neighbor = GenerateNeighbor(population[index]);
            int[] intNeighbor = Array.ConvertAll(neighbor, x => (int)x);
            double fitness = Fitness(intNeighbor);
            if (fitness < _fitnessValues[index])
            {
                population[index] = neighbor;
                _fitnessValues[index] = fitness;
                _trialValues[index] = 0;
            }
            else
            {
                _trialValues[index]++;
            }
        }
        return population;
    }

    private double[][] SendScoutBees(double[][] population)
    {
        for (int i = 0; i < NUM_SCOUTS; i++)
        {
            int index = Array.IndexOf(_trialValues, _trialValues.Max());
            population[index] = InitializeBee();
            _fitnessValues[index] = Fitness(Array.ConvertAll(population[index], x => (int)x));
            _trialValues[index] = 0;
        }
        return population;
    }

    private int[] GetBestSolution()
    {
        int index = Array.IndexOf(_fitnessValues, _fitnessValues.Min());
        return Array.ConvertAll(_population[index], x => (int)x);
    }

    private double Fitness(int[] solution)
    {
        int conflicts = 0;
        for (int i = 0; i < _graph.Length; i++)
        {
            foreach (int neighbor in _graph[i])
            {
                if (solution[i] == solution[neighbor])
                {
                    conflicts++;
                }
            }
        }
        return conflicts;
    }

    private int[] CalculateProbabilities()
    {
        int[] prob = new int[NUM_BEES];
        double sum = 0;
        for (int i = 0; i < NUM_BEES; i++)
        {
            sum += _fitnessValues[i];
        }
        for (int i = 0; i < NUM_BEES; i++)
        {
            prob[i] = (int)(_fitnessValues[i] / sum * NUM_ONLOOKERS);
        }
        return prob;
    }

    private int SelectNeighbor(int[] prob)
    {
        int index = _rand.Next(NUM_ONLOOKERS);
        for (int i = 0; i < NUM_BEES; i++)
        {
            index -= prob[i];
            if (index < 0)
            {
                return i;
            }
        }
        return NUM_BEES - 1;
    }

}
