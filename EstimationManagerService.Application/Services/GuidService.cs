using EstimationManagerService.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstimationManagerService.Application.Services;

public class GuidService : IGuidService
{
    public Guid CreateGuid() => Guid.NewGuid();
}
