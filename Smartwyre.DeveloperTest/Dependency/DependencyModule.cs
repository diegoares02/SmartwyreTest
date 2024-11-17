using Ninject.Modules;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.DataContext;
using Smartwyre.DeveloperTest.DataSeed;
using Smartwyre.DeveloperTest.Services;

namespace Smartwyre.DeveloperTest.Dependency
{
    public class DependencyModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRebateDataStore>().To<RebateDataStore>();
            Bind<IProductDataStore>().To<ProductDataStore>();
            Bind<IRebateService>().To<RebateService>();
            Bind<IDataContextSeed>().To<DataContextSeed>();
            Bind<SmartwyreDataContext>().ToSelf().InSingletonScope();
        }
    }
}
