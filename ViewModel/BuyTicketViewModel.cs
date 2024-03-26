using CinemaManagement.Models;
using CinemaManagement.Services;
using CinemaManagement.View;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Input;
using Wpf.Ui.Input;

namespace CinemaManagement.ViewModel
{
    public class TicketInfo : INotifyPropertyChanged
    {

        private int _seatNumber;

        public int SeatNumber
        {

            get
            {
                return _seatNumber;
            }
            set
            {
                _seatNumber = value;
                OnPropertyChanged(nameof(SeatNumber));
            }
        }

        private bool _isSelected;
        private bool _isEnabled;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }
        public TicketInfo(int number,bool isSelected )
        {
            _seatNumber = number;
            _isSelected = isSelected;
            if(isSelected==true)
            {
                _isEnabled = false;
            }
            else
            {
                _isEnabled = true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class VoucherInfo : INotifyPropertyChanged
    {
        public Voucher voucher { get; set; }
        private bool _isSelected;
        private bool _isEnabled;
        public bool IsSelected
        {

            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
        public int MinSubtotal { get; set; }
        public bool HasCustomLogic { get; set; }
        public VoucherInfo(Voucher voucher)
        {
            this.voucher = voucher;
            this._isSelected = false;
            this._isEnabled = false;
            if (voucher.Name == null)
            {
                this.Name = "No Name";
            }
            else
            {
                this.Name = voucher.Name;
            }
            if(voucher.Code == null)
            {
                this.Code = "";
            }
            else
            {
                this.Code = voucher.Code;
            }
            if(voucher.Amount == null)
            {
                this.Amount = 0;
            }
            else
            {
                this.Amount = (int)voucher.Amount;
            }
            if(voucher.MinSubtotal == null)
            {
                this.MinSubtotal = 0;
            }
            else
            {
                this.MinSubtotal = (int)voucher.MinSubtotal;
            }
            if(voucher.HasCustomLogic == null)
            {
                this.HasCustomLogic = false;
            }
            else
            {
                this.HasCustomLogic = (bool)voucher.HasCustomLogic;
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


    public class Information
    { 
        public int InvoiceId { get; set; }
        public string Username { get; set; }
        public string MovieName { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string ScreeningName { get; set; }
        public string BookedSeats { get; set; }
        public double SubTotal { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
        public Information(int invoiceId, string username, string movieName, string date, string startTime, string screeningName, string bookedSeats, double subTotal, double discount, double total)
        {
            InvoiceId = invoiceId;
            Username = username;
            MovieName = movieName;
            Date = date;
            StartTime = startTime;
            ScreeningName = screeningName;
            BookedSeats = bookedSeats;
            SubTotal = subTotal;
            Discount = discount;
            Total = total;
        }
    }


    public class BuyTicketViewModel : INotifyPropertyChanged
    {


        private User user;
        private Movie movie;
        private Screening screening;
        private ScreeningInfo screeningInfo;
        private MovieService movieService ;
        private ScreeningService screeningService;

        private int invoiceNumber = 0;

        public BuyTicketViewModel(Movie movie, ScreeningInfo screeningInfo,User user)
        {
            this.movie = movie;
            this.user = user;
            movieService = new MovieService();
            screeningService = new ScreeningService();

            InitComponents(screeningInfo);
            HandleVoucher();
        }
        private void  InitComponents(ScreeningInfo screeningInfo)
        {

            SubTotal = 0;
            Discount = 0;
            Total = 0;
            IsBuyButtonEnable = true;
            this.IsBuyButtonEnable = true;
            this.screeningInfo = screeningInfo;
            this.screening = screeningInfo.screening;
            this._pricePerTicket = (double)screening.Price;
           

            int totalSeats = (int)screening.Screen.Seats;

            bool[] map = new bool[totalSeats+1];

            foreach (Ticket ticket in screening.Tickets)
            {
                map[ticket.Seat] = true;
            }


            SeatList.Clear();
            SelectedSeats.Clear();
            for (int i = 0; i < totalSeats; i++)
            {
                if (map[i+1] == true)
                {
                    SeatList.Add(new TicketInfo(i + 1, true));
                }
                else
                {
                    SeatList.Add(new TicketInfo(i + 1, false));
                }
            }

        }

        private async Task HandleVoucher()
        {
            List<Voucher> vouchers = new List<Voucher>(await movieService.GetVouchers());

            for (int i = 0; i < vouchers.Count; i++)
            {
                VoucherList.Add(new VoucherInfo(vouchers[i]));
            }

            if(user.BirthDate != null)
            {
                if (IsBirthday((DateTime)user.BirthDate))
                {
                    Voucher birthdayVoucher = new Voucher();
                    birthdayVoucher.Name = "Birthday Voucher";
                    birthdayVoucher.Amount = 1000000;
                    birthdayVoucher.MinSubtotal = 0;
                    birthdayVoucher.HasCustomLogic = false;
                    VoucherList.Add(new VoucherInfo(birthdayVoucher));
                }
            }

            UpdateEnableVoucher();
        }

        static bool IsBirthday(DateTime birthday)
        {
            // Lấy ngày và tháng hiện tại
            DateTime today = DateTime.Today;
            int currentMonth = today.Month;
            int currentDay = today.Day;

            // Lấy ngày và tháng của ngày sinh nhật
            int birthMonth = birthday.Month;
            int birthDay = birthday.Day;

            // Kiểm tra xem hôm nay có phải là sinh nhật không
            return currentMonth == birthMonth && currentDay == birthDay;
        }


        private ICommand _buyButtonCommand;
        private ICommand _seatSelectedCommand;
        private ICommand _voucherSelectedCommand;

        private ObservableCollection<TicketInfo> _seatList = new ObservableCollection<TicketInfo>();
        private TicketInfo _selectedSeat;
        private ObservableCollection<TicketInfo> _selectedSeats = new ObservableCollection<TicketInfo>();

        private ObservableCollection<VoucherInfo> _voucherList = new ObservableCollection<VoucherInfo>();
        private VoucherInfo _selectedVoucher;
        private ObservableCollection<VoucherInfo> _selectedVouchers = new ObservableCollection<VoucherInfo>();



        private double _pricePerTicket;
        private double _subTotal;
        private double _total;
        private double _discount;

        private bool _isBuyButtonEnable;


        public ObservableCollection<VoucherInfo> VoucherList
        {
            get
            {
                return _voucherList;
            }
            set
            {
                _voucherList = value;
                OnPropertyChanged(nameof(VoucherList));
            }
        }

        public VoucherInfo SelectedVoucher
        {
            get
            {
                return _selectedVoucher;
            }
            set
            {
                _selectedVoucher = value;
                OnPropertyChanged(nameof(SelectedVoucher));
            }
        }
        public ObservableCollection<VoucherInfo> SelectedVouchers
        {
            get
            {
                return _selectedVouchers;
            }
            set
            {
                _selectedVouchers = value;
                OnPropertyChanged(nameof(SelectedVouchers));
            }
        }
        public ICommand VoucherSelectedCommand
        {
            get
            {
                if (_voucherSelectedCommand == null)
                {
                    _voucherSelectedCommand = new RelayCommand<VoucherInfo>(param => HandleVoucherSelection(param));
                }
                return _voucherSelectedCommand;
            }
            set
            {
                _voucherSelectedCommand = value;
            }
        }

        private async Task HandleVoucherSelection(VoucherInfo selectedVoucher)
        {
            Discount = 0;
            for(int i = 0; i < VoucherList.Count; i++)
            {
                if (VoucherList[i].IsSelected==true)
                {
                    if ( SubTotal >= VoucherList[i].MinSubtotal )
                    {
                        Discount+= VoucherList[i].Amount;
                    }
                    else
                    {
                        VoucherList[i].IsSelected = false;
                    }
                }
            }
            Total = SubTotal - Discount;
            if(Total < 0)
            {
                Total = 0;
            }
            return;
        }

        private void UpdateEnableVoucher()
        {
            for (int i = 0; i < VoucherList.Count; i++)
            {
                if (VoucherList[i].MinSubtotal > SubTotal)
                {
                    VoucherList[i].IsSelected = false;
                    VoucherList[i].IsEnabled = false;
                }
                else
                {
                    VoucherList[i].IsEnabled = true;
                }
            }
        }



        public bool IsBuyButtonEnable
        {
            get
            {
                return _isBuyButtonEnable;
            }
            set
            {
                _isBuyButtonEnable = value;
                OnPropertyChanged(nameof(IsBuyButtonEnable));
            }
        } 
        public string ScreeningName
        {
            get
            {
                return screening.Screen.Name;
            }
            set
            {
                screening.Screen.Name = value;
                OnPropertyChanged(nameof(ScreeningName));
            }
        }

        public double SubTotal
        {
            get
            {
                return _subTotal;
            }
            set
            {
                _subTotal = value;
                OnPropertyChanged(nameof(SubTotal));
            }
        }
        public double Discount
        {
            get
            {
                return _discount;
            }
            set
            {
                _discount = value;
                OnPropertyChanged(nameof(Discount));
            }
        }
        public double Total
        {
            get
            {
                return _total;
            }
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }


        public string StartTime
        {
            get
            {
                return screeningInfo.StartTime;
            }
        }
        public string EndTime
        {
            get
            {
                return screeningInfo.EndTime;
            }
        }

        public double PricePerTicket
        {
            get
            {
                return _pricePerTicket;
            }
            set
            {
                _pricePerTicket = value;
                OnPropertyChanged(nameof(PricePerTicket));
            }
        }

        public ObservableCollection<TicketInfo> SelectedSeats
        {
            get
            {
                return _selectedSeats;
            }
            set
            {
                _selectedSeats = value;
                OnPropertyChanged(nameof(SelectedSeats));
            }
        }




        public ICommand SeatSelectedCommand
        {
            get
            {
                if (_seatSelectedCommand == null)
                {
                    _seatSelectedCommand = new RelayCommand<TicketInfo>(param => HandleSeatSelection(param));
                }
                return _seatSelectedCommand;
            }
            set
            {
                _seatSelectedCommand = value;
            }
        }

        private void HandleSeatSelection(TicketInfo param)
        {
            string numberChoosen = "";
            SubTotal = 0;
            foreach (TicketInfo item in _seatList)
            {
                if (item.IsSelected && item.IsEnabled )
                {
                    SubTotal += _pricePerTicket;
                    numberChoosen += item.SeatNumber + " ";
                }
            }
            UpdateEnableVoucher();

            Total = SubTotal - Discount;
        }
        private void HandleBuyButton()
        {
            List<int> allSelectedSeats = new List<int>();

            foreach (TicketInfo item in _seatList)
            {
                if (item.IsSelected && item.IsEnabled)
                {
                    allSelectedSeats.Add(item.SeatNumber);
                }
            }
            IsBuyButtonEnable = false;

            HandleInformationInvoice(allSelectedSeats);

        }
        private async Task HandleInformationInvoice(List<int> allSelectedSeats)
        {
            Invoice invoice = await movieService.BuyTicket(allSelectedSeats, screening, user, (int) Total, (int) PricePerTicket);
            
            


        IsBuyButtonEnable = true;
            
            string bookSeat = "";
            for(int i = 0; i<allSelectedSeats.Count; i++)
            {
                bookSeat += allSelectedSeats[i] + " ";
            }
            /*if(invoice == null)
            {
                MessageBox.Show("Failed to buy ticket");
                return;
            }*/
            int invoiceId = invoice.Id;
            string username = user.Username;
            string movieName = movie.Name;
            string date = screeningInfo.Date;
            string startTime = screeningInfo.StartTime;
            string screeningName = screeningInfo.ScreenName;
            double subTotal = SubTotal;
            double discount = Discount;
            double total = Total;
            Information information = new Information(invoiceId, username, movieName, date, startTime, screeningName, bookSeat, subTotal, discount, total);
        
            InvoiceView invoiceView = new InvoiceView(information);

            Screening screeningRefresh = await screeningService.GetScreeningById(screening.Id);
            ScreeningInfo screeningInfoRefresh = new ScreeningInfo(screeningRefresh, screeningInfo.Duration);
            screeningInfo = screeningInfoRefresh;

            InitComponents(screeningInfoRefresh);

            invoiceView.ShowDialog();
        }

        public TicketInfo SelectedSeat
        {
            get
            {
                return _selectedSeat;
            }
            set
            {
                _selectedSeat = value;
                OnPropertyChanged(nameof(SelectedSeat));
            }
        }


        public ObservableCollection<TicketInfo> SeatList
        {
            get
            {
                return _seatList;
            }
            set
            {
                _seatList = value;
                OnPropertyChanged(nameof(SeatList));
            }
        }


        public ICommand BuyButtonCommand
        {
            get
            {
                if (_buyButtonCommand == null)
                {
                    _buyButtonCommand = new RelayCommand(param => HandleBuyButton());
                }
                return _buyButtonCommand;
            }
            set
            {
                _buyButtonCommand = value;
            }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
