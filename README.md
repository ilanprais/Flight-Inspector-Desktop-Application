# Flight Inspector Desktop Application
This is a flight inspector desktop application. The application works as following:  
In the **opening screen**, the user insetrs the .csv file with the flight data and the .xml file with the flight data settings, connects to the FlightGear application, which should
be opened by the user, and inserts an algorithm to detect anomaly (optional).  
// opening screen photo  
The **main screen** contains the following:  
**Scrollbar** that indicates the current time of the flight video, which is controlled by the user. The user can move the scrollbar to the exact time point that he wants to make  
the application show the data of that time point. From the scrollbar, the user can pause, play and increase the speed of the flight video.  
//photo  
**Statistics window** that shows some important data like altimeter, airspeed and more.  
**Graphs window** that shows 3 graphs about the data property that the user has chosen:
Graph of that data property, graph of the data property with is most corelative to the current data property, and graph that shows the linear regression between those two data properties.  
//photo  
**Rudder window** that contains the joystick and the rudder and throttle measures.  
//photo  

**The FlightGear application shows the flight video, while the flight inspector application shows and analyzes the data.**  
