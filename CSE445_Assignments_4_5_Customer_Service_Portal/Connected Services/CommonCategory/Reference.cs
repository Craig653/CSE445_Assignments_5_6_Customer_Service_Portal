﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSE445_Assignments_4_5_Customer_Service_Portal.CommonCategory {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="CommonCategory.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMostCommonCategory", ReplyAction="http://tempuri.org/IService1/GetMostCommonCategoryResponse")]
        string GetMostCommonCategory();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetMostCommonCategory", ReplyAction="http://tempuri.org/IService1/GetMostCommonCategoryResponse")]
        System.Threading.Tasks.Task<string> GetMostCommonCategoryAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : CSE445_Assignments_4_5_Customer_Service_Portal.CommonCategory.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<CSE445_Assignments_4_5_Customer_Service_Portal.CommonCategory.IService1>, CSE445_Assignments_4_5_Customer_Service_Portal.CommonCategory.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetMostCommonCategory() {
            return base.Channel.GetMostCommonCategory();
        }
        
        public System.Threading.Tasks.Task<string> GetMostCommonCategoryAsync() {
            return base.Channel.GetMostCommonCategoryAsync();
        }
    }
}
