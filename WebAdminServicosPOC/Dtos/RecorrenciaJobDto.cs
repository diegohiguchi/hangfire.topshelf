using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAdminServicosPOC.Dtos
{
    public class RecorrenciaJobDto
    {
        public string Id { get; set; }
        public string Cron { get; set; }
        public string Queue { get; set; }
        //public Job Job { get; set; }
        //public JobLoadException LoadException { get; set; }
        public DateTime? NextExecution { get; set; }
        public string LastJobId { get; set; }
        public string LastJobState { get; set; }
        public DateTime? LastExecution { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool Removed { get; set; }
        public string TimeZoneId { get; set; }
        public string Error { get; set; }
        public int MyProperty { get; set; }

        public RecurringScheduleType RecurringScheduleType { get; set; }
        public Servico Servico { get; set; }
        public Parceiro Parceiro { get; set; }
    }
    public class Parceiro
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public enum RecurringScheduleType
    {
        Daily,
        Hourly,
        Minutely,
        Monthly,
        Weekly,
        Yearly,
        Never
    }

    public enum Servico
    {
        Teste1,
        Teste2
    }
}
