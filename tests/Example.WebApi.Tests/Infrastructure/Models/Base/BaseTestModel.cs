namespace Example.WebApi.Tests.Infrastructure.Models.Base;

public abstract class BaseTestModel { }

public abstract class BaseTestModel<TData> : BaseTestModel
    where TData : BaseDataModel
{
    protected abstract TData DataModel { get; set; }

    public Task Execute(IServiceProvider serviceProvider)
    {
        return DataModel.RunSeedData(serviceProvider);
    }
}

public abstract class BaseScenarioTestModel<TData>(TData dataModel) : BaseTestModel
    where TData : BaseDataModel
{
    protected TData DataModel { get; init; } = dataModel;

    public Task Execute(IServiceProvider serviceProvider)
    {
        return DataModel.RunSeedData(serviceProvider);
    }
}