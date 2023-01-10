using VertexABC.Hive;

namespace VertexABC;

public class ABC
{
    private static Random _rand = new Random();

    private Graph _graph;
    private int _numColors;
    private int[][] _trialValues;
    private int _numEmployedBees;
    private int _numOnlookerBees;
    private int _numScoutBees;
    private int _limit;
    private EmployedBee[] _employedBees;
    private OnlookerBee[] _onlookerBees;
    private ScoutBee[] _scoutBees;

    public int[][] BestSolution { get; set; }

    public ABC(Graph graph, int numColors, int numEmployedBees, int numOnlookerBees, int numScoutBees, int limit)
    {
        this._graph = graph;
        this._numColors = numColors;
        _trialValues = new int[numEmployedBees][];
        for (int i = 0; i < numEmployedBees; i++)
        {
            _trialValues[i] = new int[2];
            _trialValues[i][0] = i; // Set the first element to the index of the solution
            _trialValues[i][1] = 0; // Set the second element to 0, indicating that the solution has not been evaluated
        }
        this._numEmployedBees = numEmployedBees;
        this._numOnlookerBees = numOnlookerBees;
        this._numScoutBees = numScoutBees;
        this._limit = limit;
        BestSolution = InitializeSolution();
        _employedBees = new EmployedBee[numEmployedBees];
        for (int i = 0; i < numEmployedBees; i++)
        {
            _employedBees[i] = new EmployedBee(graph);
        }
        _onlookerBees = new OnlookerBee[numOnlookerBees];
        _scoutBees = new ScoutBee[numScoutBees];
    }
    public void Solve()
    {
        for (int i = 0; i < _numScoutBees; i++)
        {
            _scoutBees[i] = new ScoutBee(_graph, _limit);
        }

        int t = 0;
        while (t < _trialValues.Length)
        {
            SendEmployedBees();
            SendOnlookerBees();
            SendScoutBees();
            UpdateBestSolution();
            t++;
        }
    }
    private void UpdateBestSolution()
    {
        int[][] currentBest = GetBestSolution();
        if (Fitness(currentBest) > Fitness(BestSolution))
        {
            BestSolution = currentBest;
        }
    }

    private int[][] GetBestSolution()
    {
        int[][] bestSolution = _employedBees[0].Solution;
        for (int i = 1; i < _numEmployedBees; i++)
        {
            int[][] thisSolution = _employedBees[i].Solution;
            if (Fitness(thisSolution) > Fitness(bestSolution))
            {
                bestSolution = thisSolution;
            }
        }
        for (int i = 0; i < _numOnlookerBees; i++)
        {
            int[][] thisSolution = _onlookerBees[i].Solution;
            if (Fitness(thisSolution) > Fitness(bestSolution))
            {
                bestSolution = thisSolution;
            }
        }
        for (int i = 0; i < _numScoutBees; i++)
        {
            int[][] thisSolution = _scoutBees[i].Solution;
            if (Fitness(thisSolution) > Fitness(bestSolution))
            {
                bestSolution = thisSolution;
            }
        }
        return bestSolution;
    }
    private double Fitness(int[][] solution)
    {
        // Calculate the number of conflicts in the solution
        int conflicts = 0;
        for (int i = 0; i < solution.Length; i++)
        {
            for (int j = 0; j < solution[i].Length; j++)
            {
                if (solution[i][j] == solution[j][i])
                    conflicts++;
            }
        }

        // Return the inverse of the number of conflicts (larger solutions will have a lower fitness)
        return 1.0D / conflicts;
    }

    private int[][] InitializeSolution()
    {
        // Initialize the solutions with random colors
        int[][] solutions = new int[_graph.Vertices.Count][];
        for (int i = 0; i < _graph.Vertices.Count; i++)
        {
            solutions[i] = new int[1];
            solutions[i][0] = _rand.Next(_numColors);
        }
        return solutions;
    }

    private double[][] CalculateProbabilities(EmployedBee[] employedBees)
    {
        // Calculate the total fitness of the solutions
        double totalFitness = 0;
        foreach (EmployedBee bee in employedBees)
        {
            totalFitness += bee.Fitness();
        }

        // Calculate the probabilities and limits of the solutions
        double[][] probabilities = new double[employedBees.Length][];
        for (int i = 0; i < employedBees.Length; i++)
        {
            probabilities[i] = new double[] { employedBees[i].Fitness() / totalFitness };
        }
        return probabilities;
    }

    public void SendOnlookerBees()
    {
        double[][] probabilities = CalculateProbabilities(_employedBees);
        for (int i = 0; i < _numOnlookerBees; i++)
        {
            int index = SelectNeighbor(probabilities);
            _onlookerBees[i] = new OnlookerBee(_graph, probabilities);
            _onlookerBees[i].Search();
        }
    }
    public void SendEmployedBees()
    {
        foreach (EmployedBee employed in _employedBees)
        {
            employed.Search();
        }
    }
    public void SendScoutBees()
    {
        foreach (ScoutBee scout in _scoutBees)
        {
            scout.Search();
        }
    }
    private int SelectNeighbor(double[][] probabilities)
    {
        double[] cumProbs = new double[probabilities.Length];
        cumProbs[0] = probabilities[0][0];
        for (int i = 1; i < probabilities.Length; i++)
        {
            cumProbs[i] = cumProbs[i - 1] + probabilities[i][0];
        }

        double randProb = _rand.NextDouble();
        for (int i = 0; i < cumProbs.Length; i++)
        {
            if (randProb < cumProbs[i])
            {
                return i;
            }
        }
        return probabilities.Length - 1;
    }
}