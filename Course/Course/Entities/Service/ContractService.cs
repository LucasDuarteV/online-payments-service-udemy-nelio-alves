using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Course.Entities;
namespace Course.Entities.Service
{
    class ContractService
    {
        public IOnlinePaymentsService _onlinePaymentsService { get; set; }

        public ContractService(IOnlinePaymentsService onlinePaymentsService)
        {
            _onlinePaymentsService = onlinePaymentsService;
        }

        public void ProcessContract(Contract contract, double months)
        {
            double basicQuota = contract.TotalValue / months;

            for (int i = 1; i <= months; i++)
            {
                DateTime dueDate = contract.Date.AddMonths(i);

                double updatedQuota =
                    basicQuota
                    + _onlinePaymentsService.Interest(basicQuota, i);

                double fullQuota =
                    updatedQuota
                    + _onlinePaymentsService.PaymentFee(updatedQuota);

                contract.AddInstallment(
                    new Installement(dueDate, fullQuota)
                );
            }
        }
     
    }
}
