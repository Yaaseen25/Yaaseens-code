using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Todo.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Todo.Views
{
    #region AppointmentEditorViewModel
    public class CalendarPageCS
    {
        /// <summary>
        /// current day meetings 
        /// </summary>
        private List<string> currentDayMeetings;

        /// <summary>
        /// minimum time meetings
        /// </summary>
        private List<string> minTimeMeetings;

        /// <summary>
        /// color collection
        /// </summary>
        private List<Color> colorCollection;

        /// <summary>
        /// list of meeting
        /// </summary>
        private ObservableCollection<Meeting> listOfMeeting;

        /// <summary>
        /// header label value
        /// </summary>
        private string headerLabelValue = DateTime.Today.Date.ToString("MMMM yyyy");

        #region ScheduleType

        /// <summary>
        /// schedule type collection
        /// </summary>
        private ObservableCollection<ScheduleTypeClass> scheduleTypeCollection = new ObservableCollection<ScheduleTypeClass>();

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="AppointmentEditorViewModel" /> class.
        /// </summary>
        public CalendarPageCS()
        {
            this.ListOfMeeting = new ObservableCollection<Meeting>();
            this.InitializeDataForBookings();
            this.BookingAppointments();
            //this.AddScheduleType();
        }

        /// <summary>
        /// Property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #region ListOfMeeting

        /// <summary>
        /// Gets or sets list of meeting
        /// </summary>
        public ObservableCollection<Meeting> ListOfMeeting
        {
            get
            {
                return this.listOfMeeting;
            }

            set
            {
                this.listOfMeeting = value;
                this.RaiseOnPropertyChanged("ListOfMeeting");
            }
        }
        #endregion

        #region HeaderLabelValue

        /// <summary>
        /// Gets or sets header label value
        /// </summary>
        public string HeaderLabelValue
        {
            get
            {
                return this.headerLabelValue;
            }

            set
            {
                this.headerLabelValue = value;
                this.RaiseOnPropertyChanged("HeaderLabelValue");
            }
        }

                /// <summary>
        /// Gets or sets schedule type collection
        /// </summary>
        public ObservableCollection<ScheduleTypeClass> ScheduleTypeCollection
        {
            get
            {
                return this.scheduleTypeCollection;
            }

            set
            {
                this.scheduleTypeCollection = value;
                this.RaiseOnPropertyChanged("ScheduleTypeCollection");
            }
        }
        #endregion

        /// <summary>
        /// method for adding schedule types
        /// </summary>
        /// 
        /*
        private void AddScheduleType()
        {
            this.ScheduleTypeCollection.Add(new ScheduleTypeClass() { ScheduleType = "Day view" });
            this.ScheduleTypeCollection.Add(new ScheduleTypeClass() { ScheduleType = "Week view" });
            this.ScheduleTypeCollection.Add(new ScheduleTypeClass() { ScheduleType = "Work week view" });
            this.ScheduleTypeCollection.Add(new ScheduleTypeClass() { ScheduleType = "Month view" });
            this.scheduleTypeCollection.Add(new ScheduleTypeClass() { ScheduleType = "Timeline view" });
        }
        */

        #region BookingAppointments

        /// <summary>
        /// Method for booking appointments.
        /// </summary>
        /// 
        
        private async void BookingAppointments()
        {
            Random randomTime = new Random();
            List<Point> randomTimeCollection = this.GettingTimeRanges();

            DateTime date;
            
            DateTime dateFrom = DateTime.Now.AddDays(-10);
            DateTime dateTo = DateTime.Now.AddDays(10);
            DateTime dateRangeStart = DateTime.Now.AddDays(0);
            TodoItemDatabase database = await TodoItemDatabase.Instance;
            //int num = await database.ConvertCountAsync();
            //all the records in the DB
            DateTime dateRangeEnd = DateTime.Now.AddDays(5);
            // date diff
            for (date = dateFrom; date < dateTo; date = date.AddDays(1))
            {
                if ((DateTime.Compare(date, dateRangeStart) > 0) && (DateTime.Compare(date, dateRangeEnd) < 0))
                {
                    for (int additionalAppointmentIndex = 0; additionalAppointmentIndex < 3; additionalAppointmentIndex++)
                    {
                        Meeting meeting = new Meeting();
                        int hour = randomTime.Next((int)randomTimeCollection[additionalAppointmentIndex].X, (int)randomTimeCollection[additionalAppointmentIndex].Y);
                        meeting.From = new DateTime(date.Year, date.Month, date.Day, hour, 0, 0);
                        meeting.To = meeting.From.AddHours(1);
                        // return name as string
                        meeting.EventName = this.currentDayMeetings[randomTime.Next(9)];
                        meeting.Color = this.colorCollection[randomTime.Next(9)];
                        meeting.IsAllDay = false;
                        meeting.StartTimeZone = string.Empty;
                        meeting.EndTimeZone = string.Empty;
                        this.ListOfMeeting.Add(meeting);
                    }
                }
                else
                {
                    /*
                    Meeting meeting = new Meeting();
                    //int hour = randomTime.Next((int)randomTimeCollection[additionalAppointmentIndex].X, (int)randomTimeCollection[additionalAppointmentIndex].Y);
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, 12, 0, 0);
                    meeting.To = meeting.From.AddHours(1);
                    meeting.EventName = this.currentDayMeetings[randomTime.Next(9)];
                    meeting.Color = this.colorCollection[randomTime.Next(9)];
                    meeting.IsAllDay = false;
                    meeting.StartTimeZone = string.Empty;
                    meeting.EndTimeZone = string.Empty;
                    this.ListOfMeeting.Add(meeting);
                    
                    Meeting meeting = new Meeting();
                    meeting.From = new DateTime(date.Year, date.Month, date.Day, randomTime.Next(9, 11), 0, 0);
                    meeting.To = meeting.From.AddDays(2).AddHours(1);
                    meeting.EventName = this.currentDayMeetings[randomTime.Next(9)];
                    meeting.Color = this.colorCollection[randomTime.Next(9)];
                    meeting.IsAllDay = false;
                    meeting.StartTimeZone = string.Empty;
                    meeting.EndTimeZone = string.Empty;
                    this.ListOfMeeting.Add(meeting);
                    */
                }
            }

            // Minimum Height Meetings
            DateTime minDate;
            DateTime minDateFrom = DateTime.Now.AddDays(-2);
            DateTime minDateTo = DateTime.Now.AddDays(2);

            for (minDate = minDateFrom; minDate < minDateTo; minDate = minDate.AddDays(1))
            {
                /*
                Meeting meeting = new Meeting();
                meeting.From = new DateTime(minDate.Year, minDate.Month, minDate.Day, randomTime.Next(9, 18), 30, 0);
                meeting.To = meeting.From;
                meeting.EventName = this.minTimeMeetings[randomTime.Next(0, 4)];
                meeting.Color = this.colorCollection[randomTime.Next(0, 10)];
                meeting.StartTimeZone = string.Empty;
                meeting.EndTimeZone = string.Empty;
                */
                Meeting meeting = new Meeting();
                //int hour = randomTime.Next((int)randomTimeCollection[additionalAppointmentIndex].X, (int)randomTimeCollection[additionalAppointmentIndex].Y);
                meeting.From = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0);
                meeting.To = meeting.From.AddHours(1);
                meeting.EventName = this.currentDayMeetings[randomTime.Next(9)];
                meeting.Color = this.colorCollection[randomTime.Next(9)];
                meeting.IsAllDay = false;
                meeting.StartTimeZone = string.Empty;
                meeting.EndTimeZone = string.Empty;
                this.ListOfMeeting.Add(meeting);

                // Setting Mininmum Appointment Height for Schedule Appointments
                if (Device.RuntimePlatform == "Android")
                {
                    meeting.MinimumHeight = 50;
                }
                else 
                {
                    meeting.MinimumHeight = 25;
                }

                this.ListOfMeeting.Add(meeting);
            }
        }

        #endregion BookingAppointments

        #region GettingTimeRanges

        /// <summary>
        /// Method for get timing range.
        /// </summary>
        /// <returns>return time collection</returns>
        private List<Point> GettingTimeRanges()
        {
            List<Point> randomTimeCollection = new List<Point>();
            randomTimeCollection.Add(new Point(9, 11));
            randomTimeCollection.Add(new Point(12, 14));
            randomTimeCollection.Add(new Point(15, 17));

            return randomTimeCollection;
        }

        #endregion GettingTimeRanges

        #region InitializeDataForBookings

        /// <summary>
        /// Method for initialize data bookings.
        /// </summary>
        private void InitializeDataForBookings()
        {

            this.currentDayMeetings = new List<string>();
            this.currentDayMeetings.Add("Jake");
            this.currentDayMeetings.Add("Sam");
            this.currentDayMeetings.Add("Charlie");
            this.currentDayMeetings.Add("Bob");
            this.currentDayMeetings.Add("Aya");
            this.currentDayMeetings.Add("Felix");
            this.currentDayMeetings.Add("Holly");
            this.currentDayMeetings.Add("May");
            this.currentDayMeetings.Add("April");
            this.currentDayMeetings.Add("Anne");

            // MinimumHeight Appointment Subjects
            this.minTimeMeetings = new List<string>();
            this.minTimeMeetings.Add("Work log alert");
            this.minTimeMeetings.Add("Birthday wish alert");
            this.minTimeMeetings.Add("Task due date");
            this.minTimeMeetings.Add("Status mail");
            this.minTimeMeetings.Add("Start sprint alert");

            this.colorCollection = new List<Color>();
            this.colorCollection.Add(Color.FromHex("#FF339933"));
            this.colorCollection.Add(Color.FromHex("#FF00ABA9"));
            this.colorCollection.Add(Color.FromHex("#FFE671B8"));
            this.colorCollection.Add(Color.FromHex("#FF1BA1E2"));
            this.colorCollection.Add(Color.FromHex("#FFD80073"));
            this.colorCollection.Add(Color.FromHex("#FFA2C139"));
            this.colorCollection.Add(Color.FromHex("#FFA2C139"));
            this.colorCollection.Add(Color.FromHex("#FFD80073"));
            this.colorCollection.Add(Color.FromHex("#FF339933"));
            this.colorCollection.Add(Color.FromHex("#FFE671B8"));
            this.colorCollection.Add(Color.FromHex("#FF00ABA9"));
        }

        #endregion InitializeDataForBookings

        #region Property Changed Event

        /// <summary>
        /// Invoke method when property changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
    #endregion

    #region ScheduleTypeClass

    /// <summary>
    /// Schedule Type Class
    /// </summary>
    public class ScheduleTypeClass : INotifyPropertyChanged
    {
        #region ScheduleType

        /// <summary>
        /// schedule type
        /// </summary>
        private string scheduleType = " ";

        /// <summary>
        /// Gets or sets schedule types
        /// </summary>
        public string ScheduleType
        {
            get
            {
                return this.scheduleType;
            }

            set
            {
                this.scheduleType = value;
                this.RaiseOnPropertyChanged("ScheduleType");
            }
        }
        #endregion

        /// <summary>
        /// property changed event handler
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invoke method when property changed
        /// </summary>
        /// <param name="propertyName">property name</param>
        private void RaiseOnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    #endregion
}
