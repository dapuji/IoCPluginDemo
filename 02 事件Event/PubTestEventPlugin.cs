using System.Collections.Generic;
using System.Drawing;
using FxEcis.T0000.IoC;
using FxEcis.T0000.IoC.Config;

namespace IoCPluginDemo._02_事件Event
{
    public class PubTestEventPlugin : BaseIoCPlugin
    {
        #region Overrides of BaseIoCPlugin

        /// <summary>初始化插件</summary>
        /// <param name="container">默认的<see cref="T:FxEcis.T0000.IoC.EcisContainer" />容器</param>
        public override void InitPlugin(EcisContainer container)
        {
            container.EventAggregator.GetEvent<ContainerEndInitEvent>().Subscribe(WhenEndInit);
        }

        private static void WhenEndInit(EcisContainer container)
        {
            //在容器初始化完毕后再发送事件，防止在注册之前就已经触发事件了
            //这里是模拟发布事件
            //正常情况下，发送事件应该在我们程序内部发送，然后插件这里只做订阅操作。
            container.EventAggregator.GetEvent<TestEvent>().Publish("测试事件已发送");
        }

        /// <summary>该插件所属分类</summary>
        public override string Module { get; } = "Demo";

        /// <summary>插件名称</summary>
        public override string Name { get; } = "发送测试事件";

        /// <summary>简单说明</summary>
        public override string Caption { get; } = "发送测试事件";

        /// <summary>详细说明</summary>
        public override string Description { get; } = "发送测试事件";

        /// <summary>详细说明图片</summary>
        public override Image DescriptionImage { get; } = null;

        /// <summary>插件配置项，用于插件数据的加载及保存</summary>
        public override List<PluginConfig> PluginConfigs { get; set; }

        #endregion
    }
}
