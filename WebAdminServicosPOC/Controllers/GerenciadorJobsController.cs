using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAdminServicosPOC.Dtos;
using WebAdminServicosPOC.Servicos;

namespace WebAdminServicosPOC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GerenciadorJobsController : ControllerBase
    {
        private IJob _job;

        public GerenciadorJobsController()
        {
            _job = new Job();
        }

        [HttpPost]
        [Route("/Criar")]
        public async Task Criar(RecorrenciaJobDto recorrenciaJobDto)
        {
            var cronType = GetCronFromRecurringType(recorrenciaJobDto.RecurringScheduleType);

            switch (recorrenciaJobDto.Servico)
            {
                case Servico.Teste1:
                    RecurringJob.AddOrUpdate(recorrenciaJobDto.Id,
                    () => _job.Task(recorrenciaJobDto.Id), cronType);
                    break;
                case Servico.Teste2:
                    RecurringJob.AddOrUpdate(recorrenciaJobDto.Id,
                    () => _job.Task(recorrenciaJobDto.Id), cronType);
                    break;
            }
        }

        [HttpPost]
        [Route("/Atualizar")]
        public async Task Atualizar(string jobId, RecurringScheduleType cron)
        {
            var cronType = GetCronFromRecurringType(cron);

            List<RecurringJobDto> recurringJobs = JobStorage.Current.GetConnection().GetRecurringJobs()
                .Where(x => x.Id.Equals(jobId)).ToList();

            foreach (var item in recurringJobs)
            {
                RecurringJob.AddOrUpdate(item.Id,
                    () => _job.Task(item.Id), cronType);
            }
        }

        [HttpGet]
        [Route("/Remover")]
        public async Task Remover(string id)
        {
            RecurringJob.RemoveIfExists(id);
        }

        [HttpPost]
        [Route("/Disparar")]
        public async Task Disparar(string id)
        {
            RecurringJob.Trigger(id);
        }

        private Func<string> GetCronFromRecurringType(RecurringScheduleType recurringSchedule)
        {
            switch (recurringSchedule)
            {
                case RecurringScheduleType.Daily:
                    return Cron.Daily;
                case RecurringScheduleType.Hourly:
                    return Cron.Hourly;
                case RecurringScheduleType.Minutely:
                    return Cron.Minutely;
                case RecurringScheduleType.Monthly:
                    return Cron.Monthly;
                case RecurringScheduleType.Weekly:
                    return Cron.Weekly;
                case RecurringScheduleType.Yearly:
                    return Cron.Yearly;
                case RecurringScheduleType.Never:
                    return Cron.Never;
                default:
                    return Cron.Daily;
            }
        }
    }
}