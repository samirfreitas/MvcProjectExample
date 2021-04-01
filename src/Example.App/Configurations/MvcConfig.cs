using Microsoft.Extensions.DependencyInjection;

namespace Example.App.Configurations
{
    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddMvc(o => {
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O Valor preenchido e invalido para este campo.");
                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "O Valor preenchido e invalido para este campo.");
                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "O Valor preenchido e invalido para este campo.");
                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "E necessario que o body na requisicao nao esteja vazio.");
                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor preenchido e invalido para este campo.");
                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor preenchido e invalido para este campo.");
                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser numerico.");
                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O valor preenchido e invalido para este campo.");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "O valor preenchido e invalido para este campo.");
                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor((x) => "Este campo precisa ser preenchido.");
                o.ModelBindingMessageProvider.ValueMustBeANumberAccessor("O Campo deve ser numerico.");

            });

            return services;
        }
    }

}
