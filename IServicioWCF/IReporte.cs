using com.msc.infraestructure.entities.reportes;
using com.msc.services.dto;
using System.Collections.Generic;
using System.ServiceModel;

namespace com.msc.services.interfaces
{
    [ServiceContract]
    public partial interface IReporte
    {
        [OperationContract]
        List<EnergiaConten> ObtEnergiaConten(pEnergiaConten param);

        [OperationContract]
        List<Trazabilidad> ObtTrazabilidad();
    }
}
