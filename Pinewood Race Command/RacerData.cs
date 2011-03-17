using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Pinewood_Race_Command
{
    public enum PrizeCategoryType
    {
        MostColorful,
        MostUnusaul,
        Ugliest
    }

    class RacerData
    {
        public RacerData()
        {
        }
    }

    [Serializable()]
    public class Racer
    {
        private int _id;
        private List<Heat> _heatList;
        private string _lastName;
        private string _firstName;
        private Car _car;
        private string _den;
        private double _averageHeatTime;
        private RaceDetail _race;

        public Racer()
        {
            _car = new Car();
            _den = string.Empty;
            _heatList = new List<Heat>();
            _race = new RaceDetail();
        }

        public Racer(string firstName, string lastName)
            : this()
        {
            _firstName = firstName;
            _lastName = lastName;
        }

        public Racer(string firstName, string lastName, string den)
            : this()
        {
            _firstName = firstName;
            _lastName = lastName;
            _den = den;
        }

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }
        

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string CarNumber
        {
            get { return _car.Number; }
            set { _car.Number = value; }
        }

        public Car PinewoodCar
        {
            get { return _car; }
            set { _car = value; }
        }

        public string Den
        {
            get { return _den; }
            set { _den = value; }
        }

        public double AverageHeatTime
        {
            get { return _averageHeatTime; }
            set { _averageHeatTime = value; }
        }

        public List<Heat> Heats
        {
            get { return _heatList; }
            set { _heatList = value; }
        }

        public RaceDetail RaceInfo
        {
            get { return _race; }
            set { _race = value; }
        }


        public void UpdateAverageHeatTime()
        {
            int heatCount = 0;
            double heatTime = 0;

            // Iterate over the Heat objects and compute average
            foreach (Heat heat in _heatList)
            {
                // Is our time > 0?
                if (heat.Time > 0)
                {
                    heatCount++;
                    heatTime += heat.Time;
                }

                _averageHeatTime = heatTime / heatCount;
            }
        }

        public override string ToString()
        {
            // Build racer display info
            return String.Concat(_firstName, " ", _lastName);
        }

        public string GetScoreboardDisplay()
        {
            return String.Concat(_firstName, " ", _lastName, " (Car #", _car.Number, ")");
        }

        public string GetOnDeckDisplay()
        {
            return String.Concat(_lastName, ": #", _car.Number);
        }
    }

    [Serializable()]
    public class Car
    {
        public int Id { get; set; }

        private string _number;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        private float _weight;

        public float Weight
        {
            get { return _weight; }
            set { _weight = value; }
        }
        private int _lossCount;

        public int LossCount
        {
            get { return _lossCount; }
            set { _lossCount = value; }
        }

        private PrizeCategoryType _prizeCategory;

        public PrizeCategoryType PrizeCategory
        {
            get { return _prizeCategory; }
            set { _prizeCategory = value; }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Car()
        {
            _number = "<enter car number>";
            _weight = 0.0f;
        }

        public Car(string number, float weight)
        {
            _number = number;
            _weight = weight;
        }

        public Car(string number, float weight, PrizeCategoryType prizeCategory)
        {
            _number = number;
            _weight = weight;
            _prizeCategory = prizeCategory;
        }

        public override string ToString()
        {
            return String.Format("Car {0}:;Weight {1}", _number, _weight);
        }
    }

    [Serializable()]
    public class Heat
    {
        private int _number;
        private int _lane;
        private double _time;

        public Heat()
        {
            _number = -1;
            _lane = -1;
            _time = 0;
        }

        public int Id { get; set; }

        public Heat(int heatNumber)
        {
            _number = heatNumber;
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }
        
        public int Lane
        {
            get { return _lane; }
            set { _lane = value; }
        }
        
        public double Time
        {
            get { return _time; }
            set { _time = value; }
        }
    }

    public class RaceDetail
    {
        private string _assignedRaceCode;
        private int _number;

        public RaceDetail()
        {
            _assignedRaceCode = "unassigned";
            _number = 0;
        }

        public string AssignedRaceCode
        {
            get { return _assignedRaceCode; }
            set { _assignedRaceCode = value; }
        }

        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        public override string ToString()
        {
            return String.Format("Race code: {0} :: Number: {1}", _assignedRaceCode, _number);
        }
    }

    public class LanePool
    {
        // Lane Stacks
        private Queue<Racer> _lane1Stack;
        private Queue<Racer> _lane2Stack;
        private Queue<Racer> _lane3Stack;
        private Queue<Racer> _lane4Stack;

        public LanePool()
        {
            _lane1Stack = new Queue<Racer>();
            _lane2Stack = new Queue<Racer>();
            _lane3Stack = new Queue<Racer>();
            _lane4Stack = new Queue<Racer>();
        }

        public Queue<Racer> Lane1Stack
        {
            get { return _lane1Stack; }
            set { _lane1Stack = value; }
        }

        public Queue<Racer> Lane2Stack
        {
            get { return _lane2Stack; }
            set { _lane2Stack = value; }
        }

        public Queue<Racer> Lane3Stack
        {
            get { return _lane3Stack; }
            set { _lane3Stack = value; }
        }

        public Queue<Racer> Lane4Stack
        {
            get { return _lane4Stack; }
            set { _lane4Stack = value; }
        }

        public int GetNumberOfRaces()
        {
            int val1 = Math.Max(_lane1Stack.Count, _lane2Stack.Count);
            int val2 = Math.Max(_lane3Stack.Count, _lane4Stack.Count);

            return Math.Max(val1, val2);
        }
    }

    public class Sweet16Racer
    {
        private Racer _racer;
        private int _seed;
        private bool _eliminated;
        private double _raceTime;

        public Sweet16Racer(Racer racer, int seed)
        {
            _racer = racer;
            _seed = seed;
        }

        public Racer Racer
        {
            get { return _racer; }
        }

        public int Seed
        {
            get { return _seed; }
        }

        public bool IsEliminated
        {
            get { return _eliminated; }
        }

        public double RaceTime
        {
            get { return _raceTime; }
            set { _raceTime = value; }
        }

        public void RecordRaceResult(bool isWinner)
        {
            _eliminated = isWinner == true ? false : true; 
        }
    }

    public class TournamentRace
    {
        private Sweet16Racer _racer1;
        private Sweet16Racer _racer2;
        private string _winnerKey;
        private string _key;

        public TournamentRace(string winnerKey)
        {
            _winnerKey = winnerKey;
        }

        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public string WinnerKey
        {
            get { return _winnerKey; }
        }

        public Sweet16Racer Racer1
        {
            get { return _racer1; }
            set { _racer1 = value; }
        }

        public Sweet16Racer Racer2
        {
            get { return _racer2; }
            set { _racer2 = value; }
        }

        public override string ToString()
        {
            return _key;
        }
    }
}
