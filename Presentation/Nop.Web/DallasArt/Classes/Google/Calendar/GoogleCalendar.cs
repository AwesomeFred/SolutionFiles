using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Google.Apis.Calendar.v3;

namespace Nop.Web.DallasArt.Classes.Google.Calendar
{
    public class GoogleCalendar
    {
        private const string CALENDARS_URL =     "//www.Google.com/calendar/feeds/default/owncalendars/full";
        private const string CALENDAR_TEMPLATE = "//www.Google.com/calendar/feeds/{0}/private/full";

        private string m_CalendarUrl = null;
        private string m_CalendarId = null;
        private readonly CalendarService m_Service = null;
        private readonly string m_CalendarName;
        private readonly string m_UserName;
        private readonly string m_Password;
        public GoogleCalendar(string calendarName, string username, string password)
        {
            m_CalendarName = calendarName;
            m_UserName = username;
            m_Password = password;
            m_Service = new CalendarService("Calendar");
        }
        public CalendarEventObject[] GetEvents()
        {
            try
            {
                if (Authenticate())
                {
                    EventQuery query = new EventQuery(m_CalendarUrl);
                    EventFeed feed = m_Service.Query(query);
                    return (from EventEntry entry in feed.Entries
                            select new CalendarEventObject()
                            {
                                Date = entry.Times[0].StartTime,
                                Title = entry.Title.Text
                            }).ToArray();
                }
                else
                {
                    return new CalendarEventObject[0];
                }
            }
            catch (Exception)
            {
                return new CalendarEventObject[0];
            }
        }

        private bool Authenticate()
        {
            m_Service.setUserCredentials(m_UserName, m_Password);
            return SaveCalendarIdAndUrl();
        }

        private bool SaveCalendarIdAndUrl()
        {
            CalendarQuery query = new CalendarQuery();
            query.Uri = new Uri(CALENDARS_URL);
            CalendarFeed resultFeed = (CalendarFeed)m_Service.Query(query);

            foreach (CalendarEntry entry in resultFeed.Entries)
            {
                if (entry.Title.Text == m_CalendarName)
                {
                    m_CalendarId = entry.Id.AbsoluteUri.Substring(63);
                    m_CalendarUrl = string.Format(CALENDAR_TEMPLATE, m_CalendarId);
                    return true;
                }
            }
            return false;
        }

        public string GetCalendarId()
        {
            return m_CalendarId;
        }
    }
}