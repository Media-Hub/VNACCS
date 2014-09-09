namespace Naccs.Core.Option
{
    using System;

    public class GatewaySettingClass
    {
        private bool gatewayadddomainname;
        private string gatewaymailbox;
        private string gatewaypopdomainname;
        private string gatewaypopservername;
        private int gatewaypopserverport;
        private string gatewaysmtpdomainname;
        private string gatewaysmtpservername;
        private int gatewaysmtpserverport;
        private bool usegateway;

        public bool GatewayAddDomainName
        {
            get
            {
                return this.gatewayadddomainname;
            }
            set
            {
                this.gatewayadddomainname = value;
            }
        }

        public string GatewayMailBox
        {
            get
            {
                return this.gatewaymailbox;
            }
            set
            {
                this.gatewaymailbox = value;
            }
        }

        public string GatewayPOPDomainName
        {
            get
            {
                return this.gatewaypopdomainname;
            }
            set
            {
                this.gatewaypopdomainname = value;
            }
        }

        public string GatewayPOPServerName
        {
            get
            {
                return this.gatewaypopservername;
            }
            set
            {
                this.gatewaypopservername = value;
            }
        }

        public int GatewayPOPServerPort
        {
            get
            {
                return this.gatewaypopserverport;
            }
            set
            {
                this.gatewaypopserverport = value;
            }
        }

        public string GatewaySMTPDomainName
        {
            get
            {
                return this.gatewaysmtpdomainname;
            }
            set
            {
                this.gatewaysmtpdomainname = value;
            }
        }

        public string GatewaySMTPServerName
        {
            get
            {
                return this.gatewaysmtpservername;
            }
            set
            {
                this.gatewaysmtpservername = value;
            }
        }

        public int GatewaySMTPServerPort
        {
            get
            {
                return this.gatewaysmtpserverport;
            }
            set
            {
                this.gatewaysmtpserverport = value;
            }
        }

        public bool UseGateway
        {
            get
            {
                return this.usegateway;
            }
            set
            {
                this.usegateway = value;
            }
        }
    }
}

