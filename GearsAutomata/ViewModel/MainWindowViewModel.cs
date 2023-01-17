using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsAutomata.Commands;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace GearsAutomata.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        //fields
        private double _vehicularWeight = 1200.0;
        private double _tyreRollingRadius = 0.3;
        private double _rollingResistance = 0.02;
        private double _adhesionCoefficient = 0.9;
        private double _drivelineEfficiency = 0.85;
        private double _height = 0.3;
        private double _aVal = 0.4;
        private double _bVal = 0.6;
        private double _lVal = 1.0;
        private double _maxEngineTorque = 120.0;
        private double _maxPower = 74570.0;
        private double _maxTopSpeed = 200.0;
        private double _maxEngineRpm = 6000.0;
        private double _kVal = 0.9;
        private double _finalDrive = 3.5;
        private double _KfVal = 0.0;
        private double _KrVal = 0.0;
        private double _highestGear = 0.0;
        private double _lowestGear = 0.0;
        private double _secondGear = 0.0;
        private double _thirdGear = 0.0;
        private double _fourthGear = 0.0;
        private double _fifthGear = 0.0;
        private double _torqueFst = 0.0;
        private double _torqueSnd = 0.0;
        private double _torqueTrd = 0.0;
        private double _torqueFourth = 0.0;
        private double _torqueFifth = 0.0;
        private double _torqueSixth = 0.0;
        private double _speedFst = 0.0;
        private double _speedSnd = 0.0;
        private double _speedTrd = 0.0;
        private double _speedFourth = 0.0;
        private double _speedFifth = 0.0;
        private double _speedSixth = 0.0;
        private bool _typeTruth = true;
        private bool _typeTruth2 = true;
        private string _variation1 = String.Empty;
        private string _variation2 = String.Empty;
        private string _variation3 = String.Empty;
        private double _gradabilityVal = 0.0;
        private ObservableCollection<ISeries> _graph1;
        private ObservableCollection<ISeries> _graph2;
        private Axis[] _torqueAxes;
        private Axis[] _speedAxes;
        private LineSeries<double> _graph3;
        private LineSeries<double> _graph4;
        private LineSeries<double> _graph5;
        private LineSeries<double> _graph6;
        
        private ObservableCollection<double> observable1;
        private ObservableCollection<double> observable2;
        private ObservableCollection<double> observable3;
        private ObservableCollection<double> observable4;
        private ObservableCollection<double> observable5;
        private ObservableCollection<double> observable6;
        
        private ObservableCollection<ISeries> seriesCollection;
        private ObservableCollection<ISeries> _Lseries;

        //properties
        public MainWindowViewModel()
        {
            CalculateLowestGearCommand = new DelegateCommand(CalculateLowestGear);
            CalculateHighestGearCommand = new DelegateCommand(CalculateTopSpeed);
            CalculateGeometricProgressionFiveSpeedCommand = new DelegateCommand(CalculateGeometricProgression);
            CalculateProgressiveCommand = new DelegateCommand(CalculateProgressive);
            CalculateGradabilityCommand = new DelegateCommand(CalculateGradability);
            TestGraphCommand = new DelegateCommand(GenerateGraph);
           


        }


        public double Gradability
        {
            get => _gradabilityVal;
            set { _gradabilityVal = value; OnPropertyChanged();}
        }

        public ObservableCollection<ISeries> Graph1
        {
            get => _graph1;
            set
            {
                _graph1 = value;
                OnPropertyChanged();
            }


        } 
        public ObservableCollection<ISeries> Graph2
        {
            get => _graph2;
            set
            {
                _graph2 = value;
                OnPropertyChanged();
            }

        }

        public Axis[] TorqueAxes
        {
            get => _torqueAxes;
            set
            {
                if (Equals(value, _torqueAxes)) return;
                _torqueAxes = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged();
            }
        }

        public Axis[] SpeedAxes
        {
            get => _speedAxes;
            set
            {
                if (Equals(value, _speedAxes)) return;
                _speedAxes = value ?? throw new ArgumentNullException(nameof(value));
                OnPropertyChanged();
            }
        }

        public ISeries[] Graph3
        {
            get;
            set;

        } 
        public ISeries[] Graph4
        {
            get;
            set;

        } 
        public ISeries[] Graph5
        {
            get;
            set;

        } 
        
        
        public Dictionary<int, double> ValueDictionary
        {
            get;
            set;
        }
        public DelegateCommand CalculateGradabilityCommand { get; private set; }
        public DelegateCommand TestGraphCommand { get; private set; }
        public DelegateCommand CalculateLowestGearCommand { get; private set; }
        public DelegateCommand CalculateHighestGearCommand { get; private set; }
        public DelegateCommand CalculateGeometricProgressionFiveSpeedCommand { get; private set; }
        public DelegateCommand CalculateProgressiveCommand { get; private set; }
        public double HighestGear
        {
            get => _highestGear;
            set
            {
                _highestGear = value;
                OnPropertyChanged();
            }
        }
        public double LowestGear
        {
            get => _lowestGear;
            set
            {
                _lowestGear = value;
                OnPropertyChanged();
            }
        }

        public double SecondGear
        {
            get => _secondGear;
            set
            {
                _secondGear = value;
                OnPropertyChanged();
            }
        }

        public double ThirdGear
        {
            get => _thirdGear;
            set
            {
                _thirdGear = value;
                OnPropertyChanged(); 
            }
        }
        public double FourthGear
        {
            get=> _fourthGear;
            set
            {
                _fourthGear = value;
                OnPropertyChanged();
            }
        }
        public double FifthGear
        {
            get => _fifthGear;
            set
            {
                _fifthGear = value;
                OnPropertyChanged();
            }
        }
        public double VehicularWeight
        {
            get => _vehicularWeight;
            set
            {
                _vehicularWeight= value;
                OnPropertyChanged();
            }
        }

        public double TyreRollingRadius
        {
            get => _tyreRollingRadius;
            set
            {
                _tyreRollingRadius = value;
                OnPropertyChanged();
            }

        }

        public double RollingResistance
        {
            get => _rollingResistance;
            set
            {
                _rollingResistance = value;
                OnPropertyChanged();
            }
        }

        public double AdhesionCoefficient
        {
            get => _adhesionCoefficient;
            set
            {
                _adhesionCoefficient = value;
                OnPropertyChanged();
            }
        }

        public double DrivelineEfficiency
        {
            get => _drivelineEfficiency;
            set
            {
                _drivelineEfficiency = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        public double AVal
        {
            get => _aVal;
            set
            {
                _aVal = value;
                OnPropertyChanged();
            }
        }

        public double BVal
        {
            get => _bVal;
            set
            {
                _bVal = value;
                OnPropertyChanged();
            }
        }

        public double LVal
        {
            get => _lVal;
            set
            {
                _lVal = value;
                OnPropertyChanged();
            }
        }

        public double MaxEngineTorque
        {
            get => _maxEngineTorque;
            set
            {
                _maxEngineTorque = value;
                OnPropertyChanged();

            }
        }

        public double MaxPower
        {
            get => _maxPower;

            set
            {
                _maxPower = value;
                OnPropertyChanged();

            }
        }

        public double MaxTopSpeed
        {
            get => _maxTopSpeed;
            set
            {
                _maxTopSpeed = value;
                OnPropertyChanged();

            }
        }

        public double MaxEngineRpm
        {
            get => _maxEngineRpm;
            set
            {
                _maxEngineRpm = value;
                OnPropertyChanged();

            }
        }

        public double KVal
        {
            get => _kVal;
            set
            {
                _kVal = value;
                OnPropertyChanged();
            }
        }

        public double FinalDrive
        {
            get => _finalDrive;
            set
            {
                _finalDrive = value;
                OnPropertyChanged();

            }
        }

        public double TorqueFst
        {
            get => _torqueFst;
            set
            {
                if (value.Equals(_torqueFst)) return;
                _torqueFst = value;
                OnPropertyChanged();
            }
        }

        public double TorqueSnd
        {
            get => _torqueSnd;
            set
            {
                if (value.Equals(_torqueSnd)) return;
                _torqueSnd = value;
                OnPropertyChanged();
            }
        }

        public double TorqueTrd
        {
            get => _torqueTrd;
            set
            {
                if (value.Equals(_torqueTrd)) return;
                _torqueTrd = value;
                OnPropertyChanged();
            }
        }

        public double TorqueFourth
        {
            get => _torqueFourth;
            set
            {
                if (value.Equals(_torqueFourth)) return;
                _torqueFourth = value;
                OnPropertyChanged();
            }
        }

        public double TorqueFifth
        {
            get => _torqueFifth;
            set
            {
                if (value.Equals(_torqueFifth)) return;
                _torqueFifth = value;
                OnPropertyChanged();
            }
        }

        public double TorqueSixth
        {
            get => _torqueSixth;
            set
            {
                if (value.Equals(_torqueSixth)) return;
                _torqueSixth = value;
                OnPropertyChanged();
            }
        }

        public double SpeedFst
        {
            get => _speedFst;
            set
            {
                if (value.Equals(_speedFst)) return;
                _speedFst = value;
                OnPropertyChanged();
            }
        }

        public double SpeedSnd
        {
            get => _speedSnd;
            set
            {
                if (value.Equals(_speedSnd)) return;
                _speedSnd = value;
                OnPropertyChanged();
            }
        }

        public double SpeedTrd
        {
            get => _speedTrd;
            set
            {
                if (value.Equals(_speedTrd)) return;
                _speedTrd = value;
                OnPropertyChanged();
            }
        }

        public double SpeedFourth
        {
            get => _speedFourth;
            set
            {
                if (value.Equals(_speedFourth)) return;
                _speedFourth = value;
                OnPropertyChanged();
            }
        }

        public double SpeedFifth
        {
            get => _speedFifth;
            set
            {
                if (value.Equals(_speedFifth)) return;
                _speedFifth = value;
                OnPropertyChanged();
            }
        }

        public double SpeedSixth
        {
            get => _speedSixth;
            set
            {
                if (value.Equals(_speedSixth)) return;
                _speedSixth = value;
                OnPropertyChanged();
            }
        }

        public bool TypeTruth
        {
            get => _typeTruth;
            set
            {
                _typeTruth = value;
                OnPropertyChanged();
                Trace.WriteLine(_typeTruth);

            }
        }
        public bool TypeTruth2
        {
            get => _typeTruth2;
            set
            {
                _typeTruth2 = value;
                OnPropertyChanged();
            }
        }
        
        
        public string Variation1
        {
            get => _variation1;
            set
            {
                _variation1 = value;
                OnPropertyChanged();
            }
        }

        public string Variation2
        {
            get => _variation2;
            set
            {
                _variation2 = value;
                OnPropertyChanged();
            }
        }

        public string Variation3
        {
            get => _variation3;
            set
            {
                _variation3 = value;
                OnPropertyChanged();
            }
        }

        


        double calculateKF()
        {
            Trace.WriteLine((float)(_bVal + (_height * _rollingResistance)) / ((_lVal + (_adhesionCoefficient * _height))));
            return (_bVal + (_height * _rollingResistance)) / ((_lVal + (_adhesionCoefficient * _height)));
        }
        double calculateKR()
        {
            return (_aVal - (_height * _rollingResistance)) / (_lVal - (_adhesionCoefficient * _height));
        }

        void CalculateLowestGear(object perimeter)
        {
            SecondGear = 0;
            ThirdGear = 0;
            FourthGear = 0;
            FifthGear = 0;
            Variation3 = "";
            var driveTrain = perimeter as string;
            if (driveTrain == "FWD")
            {
                Variation1 = "FWD";
                LowestGear = MathsCalc.Formulaes.LowestGear(calculateKF(), _tyreRollingRadius, _drivelineEfficiency, _maxEngineTorque, _adhesionCoefficient, _vehicularWeight);
               
            }
            else
            {
                Variation1 = "RWD";
                LowestGear = MathsCalc.Formulaes.LowestGear(calculateKR(), _tyreRollingRadius, _drivelineEfficiency, _maxEngineTorque, _adhesionCoefficient, _vehicularWeight);
            }
            
        }

      

        void CalculateTopSpeed(object perimeter)
        {
            SecondGear = 0;
            ThirdGear = 0;
            FourthGear = 0;
            FifthGear = 0;
            Variation3 = "";
            var msg = perimeter as string;
            if (msg == "Fixed")
            {
                Variation2 = "Fixed";
                HighestGear = MathsCalc.Formulaes.GenericTopSpeed(_maxEngineRpm, _tyreRollingRadius, _maxTopSpeed * 0.278);
            }
            
            else if (msg == "Max")
            {
                Variation2 = "Max";
                var V = MathsCalc.Formulaes.MaxTopSpeedHelper(_maxPower, _drivelineEfficiency, _rollingResistance,
                    _vehicularWeight);
                HighestGear = MathsCalc.Formulaes.GenericTopSpeed(_maxEngineRpm, _tyreRollingRadius, V);
            }
            
        }
        void CalculateGeometricProgression(object perimeter)
        {
            var msg = perimeter as string;
            if (msg == "5S")
            {
                Variation3 = "GP5S";
                var cgp = MathsCalc.Formulaes.calculateCGP(_lowestGear, _highestGear, 0.25);
                var (x, y, z) = MathsCalc.Formulaes.CGP5(_highestGear, cgp);
                SecondGear = x;
                ThirdGear = y;
                FourthGear = z;
                CalculateMaxTorque("5SPEED");
                CalculateMaxSpeed("5SPEED");
            }
            else
            {
                Variation3 = "GP6S";
                var cgp = MathsCalc.Formulaes.calculateCGP(_lowestGear, _highestGear, 0.2);
                var (x, y, z, aaItem4) = MathsCalc.Formulaes.CGP6(_highestGear, cgp);
                SecondGear = x;
                ThirdGear = y;
                FourthGear = z;
                FifthGear = aaItem4;
                CalculateMaxTorque("6SPEED");
                CalculateMaxSpeed("6SPEED");

            }


        }

        void CalculateProgressive(object parameter)
        {
            var msg = parameter as string;
            if (msg == "5S")
            {

                Variation3 = "P5S";
                var cgp = MathsCalc.Formulaes.calculateCGP(_lowestGear, _highestGear, 0.25);
                var (g2, g3, g4) = MathsCalc.Formulaes.ProgressiveFiveSpeed(_kVal, _lowestGear, cgp);
                SecondGear = g2;
                ThirdGear = g3;
                FourthGear = g4;
                CalculateMaxTorque("5SPEED");
                CalculateMaxSpeed("5SPEED");
            }
            else
            {
                Variation3 = "P6S";
                var cgp = MathsCalc.Formulaes.calculateCGP(_lowestGear, _highestGear, 0.2);
                var (g2, g3, g4,g5) = MathsCalc.Formulaes.ProgressiveSixSpeed(_kVal, _lowestGear, cgp);
                SecondGear = g2;
                ThirdGear = g3;
                FourthGear = g4;
                FifthGear = g5;
                CalculateMaxTorque("6SPEED");
                CalculateMaxSpeed("6SPEED");
            }
         
        }

        void CalculateGradability(object parameter)
        {
            var msg = parameter as string;
            Trace.WriteLine(msg);
            if (msg == "FWD")
                Gradability = MathsCalc.Formulaes.Gradibility(calculateKF(), _adhesionCoefficient, _rollingResistance);
            else if(msg == "RWD")
                Gradability = MathsCalc.Formulaes.Gradibility(calculateKR(), _adhesionCoefficient, _rollingResistance);

        }

        void CalculateMaxSpeed(object parameter)
        {
            if (parameter == "5SPEED")
            {
                SpeedSixth = 0.0;
                var gears= new List<double> { LowestGear,SecondGear,ThirdGear,FourthGear,HighestGear };
                var result = from gear in gears
                    select
                        MathsCalc.Formulaes.MaxVelocity(_finalDrive,_drivelineEfficiency,_tyreRollingRadius,_maxEngineRpm,gear);
                var resultList = result.ToList();
                SpeedFst = resultList[0];
                SpeedSnd = resultList[1];
                SpeedTrd = resultList[2];
                SpeedFourth =resultList[3];
                SpeedFifth = resultList[4];
            }
            else if (parameter == "6SPEED")
            {
                var gears= new List<Double> { LowestGear,SecondGear,ThirdGear,FourthGear,FifthGear,HighestGear };
                var result = from gear in gears
                    select
                        MathsCalc.Formulaes.MaxVelocity(_finalDrive,_drivelineEfficiency,_tyreRollingRadius,_maxEngineRpm,gear);
                var resultList = result.ToList();
                SpeedFst = resultList[0];
                SpeedSnd = resultList[1];
                SpeedTrd = resultList[2];
                SpeedFourth =resultList[3];
                SpeedFifth = resultList[4];
                SpeedSixth = resultList[5];
            }
           
        }
        void CalculateMaxTorque(object parameter)
        {
            //6Speed
            if (parameter as string == "5SPEED")
            {
                TorqueSixth = 0;
                var iterable = new List<double> { LowestGear,SecondGear,ThirdGear,FourthGear,HighestGear };
                var result = from x in iterable
                    select
                        MathsCalc.Formulaes.TorqueForce(_drivelineEfficiency,_finalDrive,_tyreRollingRadius,x,_maxEngineTorque);
                var resultList = result.ToList();
                TorqueFst = resultList[0];
                TorqueSnd = resultList[1];
                TorqueTrd = resultList[2];
                TorqueFourth =resultList[3];
                TorqueFifth = resultList[4];
            }else if (parameter as string == "6SPEED")
            {
                var iterable = new List<Double> { LowestGear,SecondGear,ThirdGear,FourthGear,FifthGear,HighestGear };
                var result = from x in iterable
                    select
                        MathsCalc.Formulaes.TorqueForce(_drivelineEfficiency,_finalDrive,_tyreRollingRadius,x,_maxEngineTorque);
                var resultList = result.ToList();
                TorqueFst = resultList[0];
                TorqueSnd = resultList[1];
                TorqueTrd = resultList[2];
                TorqueFourth =resultList[3];
                TorqueFifth = resultList[4];
                TorqueSixth = resultList[5];

            }
            
            
        }

        void GenerateGraph(object parameter)
        {
            Trace.WriteLine("ACTIVATED!");
            
            Graph1 = new ObservableCollection<ISeries> { new LineSeries<Double> {  
                DataLabelsSize = 15,
                DataLabelsPaint = new SolidColorPaint(SKColors.DimGray),
                // all the available positions at:
                // https://lvcharts.com/api/2.0.0-beta.700/LiveChartsCore.Measure.DataLabelsPosition
                DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Bottom,
                // The DataLabelsFormatter is a function 
                // that takes the current point as parameter
                // and returns a string.
                // in this case we returned the PrimaryValue property as currency
                DataLabelsFormatter = (point) => point.PrimaryValue.ToString("F"),
                TooltipLabelFormatter = point => $"{point.PrimaryValue:F1}",
                LineSmoothness = 0 ,
                Values = new ObservableCollection<double> {TorqueFst,TorqueSnd,TorqueTrd,TorqueFourth,TorqueFifth,TorqueSixth } } };
            Graph2 = new ObservableCollection<ISeries> { new LineSeries<Double>
            {
                DataLabelsSize = 15,
                DataLabelsPaint = new SolidColorPaint(SKColors.DimGray),
                // all the available positions at:
                // https://lvcharts.com/api/2.0.0-beta.700/LiveChartsCore.Measure.DataLabelsPosition
                DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Bottom,
                // The DataLabelsFormatter is a function 
                // that takes the current point as parameter
                // and returns a string.
                // in this case we returned the PrimaryValue property as currency
                DataLabelsFormatter = (point) => point.PrimaryValue.ToString("F"),
                TooltipLabelFormatter = point => $"{point.PrimaryValue:F1}",
                Values = new ObservableCollection<double> {SpeedFst,SpeedSnd,SpeedTrd,SpeedFourth,SpeedFifth,SpeedSixth }
            } };
            TorqueAxes = new Axis[]
            {
                new Axis
                {
                    Name = $"Torque For Each Gear[{_variation1},{_variation2} {_variation3}]",
                    NamePaint = new SolidColorPaint(SKColors.Black), 

                    LabelsPaint = new SolidColorPaint(SKColors.Blue), 
                    TextSize = 10,

                    
                    Labels = new string[] { "1","2","3","4","5","6" }, 
                    
                }
            };
            SpeedAxes = new Axis[]
            {
                new Axis
                {
                    Name = $"Speed For Each Gear [{_variation1},{_variation2} {_variation3}]",
                    NamePaint = new SolidColorPaint(SKColors.Black), 

                    LabelsPaint = new SolidColorPaint(SKColors.Blue), 
                    TextSize = 10,
                    Labels = new string[] { "1","2","3","4","5","6" }, 

                    
                }
            };
        }
        



    }
}
