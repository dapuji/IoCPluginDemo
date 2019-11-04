using FxEcis.T0000.IoC.Events;

namespace IoCPluginDemo._02_事件Event
{
    /// <summary>
    ///     这里定义一个事件（正常来说，这个事件是在我们主程序里面定义的）
    /// </summary>
    public class TestEvent 
        : PubSubEvent<string>
    //定义事件，要继承 PubSubEvent 或者 PubSubEvent<T>
    //PubSubEvent 表示该事件不需要任何参数
    //PubSubEvent<T> 表示该事件有参数 T
    {
        //这个类不需要写任何方法，继承的目的是为了区分各个事件，以及知道它们的意义。
    }
}