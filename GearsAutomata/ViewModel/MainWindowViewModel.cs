using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GearsAutomata.Commands;

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
        private double _finalDrive = 0.0;
        private double _KfVal = 0.0;
        private double _KrVal = 0.0;
        private double _highestGear = 0.0;
        private double _lowestGear = 0.0;
        private double _secondGear = 0.0;
        private double _thirdGear = 0.0;
        private double _fourthGear = 0.0;
        private double _fifthGear = 0.0;
        private bool _typeTruth = true;
        private bool _typeTruth2 = true;
        private string _variation1 = String.Empty;
        private string _variation2 = String.Empty;
        private string _variation3 = String.Empty;

        //properties
        public MainWindowViewModel()
        {
            CalculateLowestGearCommand = new DelegateCommand(CalculateLowestGear);
            CalculateHighestGearCommand = new DelegateCommand(CalculateTopSpeed);
            CalculateGeometricProgressionFiveSpeedCommand = new DelegateCommand(CalculateGeometricProgression);
            CalculateProgressiveCommand = new DelegateCommand(CalculateProgressive);
        }
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
            Trace.WriteLine((float) (_bVal + (_height * _rollingResistance) / (_lVal + (_adhesionCoefficient * _height))));
            return (_bVal + (_height * _rollingResistance)) / ((_lVal + (_adhesionCoefficient * _height)));
        }
        double calculateKR()
        {
            return (_aVal - (_height * _rollingResistance)) / (_lVal - (_adhesionCoefficient * _height));
        }

        void CalculateLowestGear(object perimeter)
        {
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
            }
         
        }




    }
}
