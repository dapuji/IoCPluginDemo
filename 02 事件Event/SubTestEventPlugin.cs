using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FxEcis.T0000.IoC;
using FxEcis.T0000.IoC.Config;
using FxEcis.T0000.IoC.Events;

namespace IoCPluginDemo._02_事件Event
{
    public class SubTestEventPlugin : BaseIoCPlugin
    {
        /// <summary>
        ///     这里需要是静态方法，防止订阅被回收。
        ///     或者调用 Subscribe 方法时，keepSubscriberReferenceAlive 参数改成 true。
        /// </summary>
        /// <param name="data"></param>
        public static void Test(string data)
        {
            MessageBox.Show("接收到数据：" + data);
        }

        #region Overrides of BaseIoCPlugin

        /// <summary>初始化插件</summary>
        /// <param name="container">默认的<see cref="T:FxEcis.T0000.IoC.EcisContainer" />容器</param>
        public override void InitPlugin(EcisContainer container)
        {
            //获取到TestEvent事件
            var @event = container.EventAggregator.GetEvent<TestEvent>();

            //订阅事件。
            //如果在插件中，Subscribe注册的方法，需要是静态方法，防止订阅被回收。
            //或者调用 Subscribe 方法时，keepSubscriberReferenceAlive 参数改成 true。
            //或者用其它方式自己管理该方法，如静态类等来保持该方法不会被.net的垃圾回收器回收。
            //订阅事件的方法，有多个重载，可自己查看。
            @event.Subscribe(Test, ThreadOption.PublisherThread);

            //订阅可以进行过滤。
            //比如说，这里判断事件的传参不是空值的情况下，才调用Test方法。
            @event.Subscribe(Test, ThreadOption.PublisherThread,
                             true, 
                             s => !string.IsNullOrWhiteSpace(s));
        }

        /// <summary>该插件所属分类</summary>
        public override string Module { get; } = "Demo";

        /// <summary>插件名称</summary>
        public override string Name { get; } = "接受测试事件";

        /// <summary>简单说明</summary>
        public override string Caption { get; } = "接受测试事件";

        /// <summary>详细说明</summary>
        public override string Description { get; } = "接受测试事件";

        /// <summary>详细说明图片</summary>
        public override Image DescriptionImage { get; } = null;

        /// <summary>插件配置项，用于插件数据的加载及保存</summary>
        public override List<PluginConfig> PluginConfigs { get; set; }

        #endregion
    }
}