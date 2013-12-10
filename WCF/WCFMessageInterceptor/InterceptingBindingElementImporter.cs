using System.ServiceModel.Description;

namespace WSO2.WCF.MessageInterceptor
{
    public abstract class InterceptingBindingElementImporter: IPolicyImportExtension
    {
        void IPolicyImportExtension.ImportPolicy(MetadataImporter importer, PolicyConversionContext context)
        {
            ChannelMessageInterceptor messageInterceptor = CreateMessageInterceptor();
            messageInterceptor.OnImportPolicy(importer, context);
        }

        protected abstract ChannelMessageInterceptor CreateMessageInterceptor();
    }
}
