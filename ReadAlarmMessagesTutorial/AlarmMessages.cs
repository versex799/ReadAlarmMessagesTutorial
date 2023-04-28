using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadAlarmMessagesTutorial
{
    public class AlarmMessages
    {
        short _ret = 0;

        public List<string> GetActiveAlarms(ushort handle)
        {
            List<string> alarms = new List<string>();
            short numberOfAlarms = 10;
            Focas1.ODBALMMSG2 allAlarms = new Focas1.ODBALMMSG2();

            if (handle == 0)
                return alarms;

            _ret = Focas1.cnc_rdalmmsg2(handle, -1, ref numberOfAlarms, allAlarms);

            if (_ret != Focas1.EW_OK)
            {
                Console.WriteLine($"Error reading Alarms: {_ret}");
                return alarms;
            }

            var rawAlarms = AlarmsToList(allAlarms);

            for (int i = 0; i < rawAlarms.Count; i++)
            {
                var formattedAlarm = GetAlarmText(rawAlarms[i]);
                alarms.Add(formattedAlarm);
            }

            return alarms;
        }

        private List<Focas1.ODBALMMSG2_data> AlarmsToList(Focas1.ODBALMMSG2 rawAlarms)
        {
            return new List<Focas1.ODBALMMSG2_data>()
            {
                rawAlarms.msg1,
                rawAlarms.msg2,
                rawAlarms.msg3,
                rawAlarms.msg4,
                rawAlarms.msg5,
                rawAlarms.msg6,
                rawAlarms.msg7,
                rawAlarms.msg8,
                rawAlarms.msg9,
                rawAlarms.msg10
            };
        }

        private string GetAlarmText(Focas1.ODBALMMSG2_data msg)
        {

            if (msg == null)
                return "NO ALARM";

            if (msg.type == 0 && msg.alm_no == 0)
                return "NO ALARM";
            else
                return $"{_alarmType[msg.type]}{msg.alm_no}: {msg.alm_msg.Substring(0, msg.msg_len)}";
        }

        private static Dictionary<short, string> _alarmType = new Dictionary<short, string>()
        {
            { 0, "SW" },
            { 1, "PW" },
            { 2, "IO" },
            { 3, "PS" },
            { 4, "OT" },
            { 5, "OH" },
            { 6, "SV" },
            { 7, "SV" },
            { 8, "MC" },
            { 9, "SP" },
            { 10, "DS" },
            { 11, "IE" },
            { 12, "BG" },
            { 13, "SN" },
            { 15, "EX" },
            { 19, "PC" },
        };
    }
}
