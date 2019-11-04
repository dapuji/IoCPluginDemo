using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FxEcis.T0000.IoC;
using FxEcis.T0000.IoC.Config;

namespace IoCPluginDemo._01_基础demo
{
    /// <summary>
    ///     演示IoC如何解析数据
    /// </summary>
    public class ResolveIoCPlugin : BaseIoCPlugin
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
            //一般来说，插件这里是不需要解析实例的，即调用 EcisContainer.Resolve 方法。因为很少用得到。
            //这里只是为了显示如何在程序内解析实例的。
            //请勿直接在这里调用 EcisContainer.Resolve 方法，会导致内部容器被锁定。
            //插件的载入顺序不是固定的，如果这里直接解析了类型，如果下一个插件还需要注册类型，将导致程序直接抛出异常。
            //所以这里需要订阅 ContainerEndInitEvent 事件（容器已初始化完毕事件），然后再进行解析操作。
            container.EventAggregator.GetEvent<ContainerEndInitEvent>().Subscribe(ResolveWhenEndInit);
        }

        private static void ResolveWhenEndInit(EcisContainer container)
        {
            //因为容器都已经初始化了，全部的插件也都载入完成了。这里已经可以解析实例。

            //用于判断有没有注册这个类型
            container.IsRegister<ISaveTriageInfo>();
            container.IsRegisterCollection<ISaveTriageInfo>();

            //这里获取到的是 SaveTriageInfo1
            //因为RegisterIoCPlugin.cs中，调用的是
            //  container.Register<ISaveTriageInfo,SaveTriageInfo1>();
            //如果没注册过该类型，直接解析的话会抛出异常。
            var saveTriageInfo = container.Resolve<ISaveTriageInfo>();
            saveTriageInfo.Save(null);

            //这里获取到的是 SaveTriageInfo1 ，SaveTriageInfo2
            //因为RegisterIoCPlugin.cs中，调用的是
            //  container.RegisterCollection<ISaveTriageInfo>(new []{typeof(SaveTriageInfo1),typeof(SaveTriageInfo2) });
            var saveTriageInfos = container.ResolveAll<ISaveTriageInfo>();
            foreach (var triageInfo in saveTriageInfos)
            {
                triageInfo.Save(null);
            }

            //这样也可以解析集合。
            var saveTriageInfos2 = container.Resolve<IEnumerable<ISaveTriageInfo>>().ToList();
            foreach (var triageInfo in saveTriageInfos2)
            {
                triageInfo.Save(null);
            }

            //注意，如果只调用了container.Register来注册，只能用container.Resolve来解析。
            //如果只调用了container.RegisterCollection来注册，只能用container.ResolveAll或Resolve<IEnumerable<>>来解析。
            //否则解析不到数据，并且会抛出异常。
        }

        /// <summary>该插件所属分类，中英文均可，仅用于维护界面上的分类。</summary>
        public override string Module { get; } = "Demo";

        /// <summary>插件名称，建议英文名称，并与其它插件互斥（不强制要求），名称请勿太长</summary>
        public override string Name { get; } = "Demo_ResolveIoC";

        /// <summary>简单说明，建议中文说明，尽量简短</summary>
        public override string Caption { get; } = "解析IoC";

        /// <summary>详细说明，长度不限，应该尽量说明该插件的具体作用。</summary>
        public override string Description { get; } = "IoC插件，演示如何解析接口，无实际功能";

        /// <summary>详细说明图片，如果是涉及到界面上的插件，建议截图说明该插件的作用。</summary>
        public override Image DescriptionImage { get; } = null;

        /// <summary>插件配置项，用于插件数据的加载及保存</summary>
        public override List<PluginConfig> PluginConfigs { get; set; }

        #endregion
    }
}
