/*
    Author: Christopher Angulo
    Description: This file contains the interface of a WSDL service. This application allows its user to check if a
    URL has been used by both Service 1 and Service 14.
    Class: ASU CSE-445 - Service Oriented Computing - Fall 2024
    Professor: Yinong Chen
    Last Date Modified: 10/20/2024
 */


// Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Serv1Serv14Compare
{
    [ServiceContract]
    public interface IService1
    {
        // this created by default, I'm just going to leave it alone
        [OperationContract]
        string GetData(int value);

        // this created by default, I'm just going to leave it alone
        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // check if a URL has been used by both Service 1 and Service 14
        [OperationContract]
        bool MostCommon();

        [OperationContract]
        string GetCurrentXML();
    }

    // this created by default, I'm just going to leave it alone
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
