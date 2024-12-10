//namespace ReactWithDotNet.WebSite.Pages;

//class StateCount
//{
//    public int Count { get; set; }
//}
//class A : Component<StateCount>
//{
//    protected override Task constructor()
//    {
//        Client.ListenEvent("XXX", OnCalled);
        
//        return base.constructor();
//    }

//    Task OnCalled()
//    {
//        state.Count++;
        
//        return Task.CompletedTask;
//    }

//    protected override Element render()
//    {
//        return new div { state.Count.ToString() };
//    }
//}

//class B : Component<StateCount>
//{
//    protected override Task componentWillUnmount()
//    {
//        Client.DispatchEvent("XXX");
        
//        return Task.CompletedTask;
//    }

//    protected override Element render()
//    {
//        return new div { state.Count.ToString() };
//    }
//}

//class BContainer : Component<StateCount>
//{
   

//    protected override Element render()
//    {
//        return new div
//        {
//            new A(),
            
//            new div{"Add", OnClick(OnAdd)},
            
//            Enumerable.Range(0,state.Count).Select(i =>
//            {
//                return new B();
//            }),
            
//            new div{"Remove", OnClick(OnRemove)},
            
            
            
            
//        };
//    }

//    Task OnAdd(MouseEvent e)
//    {
//        state.Count++;
        
//        return Task.CompletedTask;
//    }
    
//    Task OnRemove(MouseEvent e)
//    {
//        if (state.Count is 0)
//        {
//            return Task.CompletedTask;
//        }
//        state.Count--;
        
//        return Task.CompletedTask;
//    }
//}


//class PageMain : PureComponent
//{
//    protected override Element render()
//    {
//        return new div(Size(300))
//        {
//            new BContainer(),
//        };
//    }
//}