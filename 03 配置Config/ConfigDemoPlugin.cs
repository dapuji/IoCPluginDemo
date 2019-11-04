using System.Collections.Generic;
using System.Drawing;
using FxEcis.T0000.IoC;
using FxEcis.T0000.IoC.Config;

namespace IoCPluginDemo._03_配置Config
{
    public class ConfigDemoPlugin : BaseIoCPlugin
    {
        #region Overrides of BaseIoCPlugin

        /// <summary>
        ///     初始化插件。
        ///     请勿在此方法调用<see cref="M:FxEcis.T0000.IoC.EcisContainer.Resolve(System.Type)" />、<see cref="M:FxEcis.T0000.IoC.EcisContainer.ResolveAll(System.Type)" />方法，这会导致容器锁定，后续的注册会失败。
        ///     如需要解析类型，应该使用<see cref="P:FxEcis.T0000.IoC.EcisContainer.EventAggregator" />获取<see cref="T:FxEcis.T0000.IoC.ContainerEndInitEvent" />事件，然后在事件触发后解析
        /// </summary>
        /// <param name="container">默认的<see cref="T:FxEcis.T0000.IoC.EcisContainer" />容器</param>
        public override void InitPlugin(EcisContainer container)
        {
        }

        /// <summary>该插件所属分类，中英文均可，仅用于维护界面上的分类。</summary>
        public override string Module { get; } = "Demo";

        /// <summary>插件名称，建议英文名称，并与其它插件互斥（不强制要求），名称请勿太长</summary>
        public override string Name { get; } = "Demo_Config";

        /// <summary>简单说明，建议中文说明，尽量简短</summary>
        public override string Caption { get; } = "配置测试";

        /// <summary>详细说明，长度不限，应该尽量说明该插件的具体作用。</summary>
        public override string Description { get; } = "配置测试";

        /// <summary>详细说明图片，如果是涉及到界面上的插件，建议截图说明该插件的作用。</summary>
        public override Image DescriptionImage { get; } = null;

        /// <summary>插件配置项，用于插件数据的加载及保存</summary>
        public override List<PluginConfig> PluginConfigs { get; set; } = new List<PluginConfig>()
        {
            //这里的配置项，是默认值，这里添加后，会显示到界面上，允许实施配置。
            new PluginConfig
            {
                Caption = "这里是配置1的说明",
                Key     = "Config1",
                Value   = "Config1的数据"
            },
            new PluginConfig
            {
                Caption = "这里是配置2的说明",
                Key     = "Config2",
                Value   = "Config2的数据"
            },
            new PluginConfig
            {
                Caption = "这里是配置3的说明",
                Key     = "Config3",
                Value   = "Config3的数据"
            }
        };

        /// <summary>获取配置维护界面（若有），返回null则使用默认配置界面</summary>
        /// <returns></returns>
        public override BaseConfigControl GetConfigControl()
        {
            //可以重写该方法，然后返回一个自定义的配置界面。
            //或者不重写该方法，则使用默认的配置界面
            return new ConfigControl();
        }

        #endregion
    }
}
