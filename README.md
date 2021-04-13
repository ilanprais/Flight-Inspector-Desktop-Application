# Flight Inspector Desktop Application
About the application
---------------------
This is a flight inspector desktop application. The application works as following:  
In the **opening screen**, the user insetrs the .csv file with the flight data and the .xml file with the flight data settings, connects to the FlightGear application, which should be opened by the user, and inserts an algorithm to detect anomaly (optional). Then, the application moves to the **main screen**.    
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
  
About the project structure
---------------------------
The project contains 3 directories:  
**Model directory** which contains the class that are related to the model. **FlightGearModel** is the facade model class, which implements the **IFlightGearModel** interface.  
This class contains an instance of an object that implements the **IAsyncFGClient** interface, and uses that object to communicate with the FlightGear asynchronously.  
In this way, the **IFlightGearModel** and **IAsyncFGClient** interfaces are built together in the **Bridge design pattern**.  
**ViewModel directory** which contains a ViewModel for every user control, for example - the **PlayBackViewModel** is a ViewModel for the playback scrollbar, which gets from the  
model the data which is relevant to the scrollbar (like the flight video speed), and decorates the relevant operations of the model for the scrollbar, like *Render* which starts  
the rendering of the flight data.  
**View directory** which contains the .xaml file and the code behind for every user controls and window.  

