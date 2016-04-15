# HappyStore
April 14~15, 2016 TE hackathon in singapore
Automatically find the optimum temperature and humidity for shopping

#Objective
We have focused on how we would increase customer's satisfaction in the store. To do that, we developed client side app on windows 10 IoT on Rasberry PI 2 and weather sensors(for temperature, humidity) but we should have gotten web cam. becuase of limited resource, we emulated taking picutre as a few of still images. these images would be sent to microsoft cognitive service, especially, emotion service, to estimate customer's satisfaction. this all information includes temperature, humidity, time, location information would be sent to IoT Hub for store persistent storage(e.g. Azure Database). As a result, Power BI was used to show the insight of relationship customer's satisation and lots of factors. 

This repo is only used for TE hackathon purpose. 

Features we use
- Rasberry PI 2
- Windows 10 IoT
- Azure IoT Hub
- Cognitive Service(Emotion)
- Power BI Embedded
- Auzre Cloud Service - WorkRole

