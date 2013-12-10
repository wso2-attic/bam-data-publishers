using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace WSO2.WCF.MessageInterceptor
{
    public abstract class InterceptingElement : BindingElementExtensionElement
    {
        protected InterceptingElement()
            : base()
        {
        }

        public override Type BindingElementType
        {
            get
            {
                return typeof(InterceptingBindingElement);
            }
        }

        protected abstract ChannelMessageInterceptor CreateMessageInterceptor();

        protected override BindingElement CreateBindingElement()
        {
            return new InterceptingBindingElement(CreateMessageInterceptor());
        }
    }
}