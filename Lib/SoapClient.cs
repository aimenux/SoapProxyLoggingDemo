using System.ServiceModel;
using System.ServiceModel.Channels;
using DataFlexSoapWebService;

namespace Lib;

public class SoapClient : InfoSoapTypeClient, ISoapClient
{
    public SoapClient(EndpointConfiguration endpointConfiguration) : base(endpointConfiguration)
    {
    }

    public SoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : base(endpointConfiguration, remoteAddress)
    {
    }

    public SoapClient(EndpointConfiguration endpointConfiguration, EndpointAddress remoteAddress) : base(endpointConfiguration, remoteAddress)
    {
    }

    public SoapClient(Binding binding, EndpointAddress remoteAddress) : base(binding, remoteAddress)
    {
    }
}