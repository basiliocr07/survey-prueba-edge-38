
using MediatR;
using SurveyApp.Domain.Models;
using System.Collections.Generic;

namespace SurveyApp.Application.Customers.Queries.GetAllServices
{
    public class GetAllServicesQuery : IRequest<IEnumerable<Service>>
    {
    }
}
