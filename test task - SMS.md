# Interview test task
The objective of this this project is to create an API and a service for sending SMSs.

You'll need to create 2 projects:
 - 1 API project that will receive requests from a user
 - 1 console application project that will wait for messages to be processed and save processed messages to a persistent storage.

Communication between the 2 applications is to be implemented using a message bus of your choice. Objects storage is to be a relational database of your choice.

*SMS class*
```
public class SMS
{
    public Guid Id {get; set; }
    public string From { get; set; }
    public string[] To { get; set; }
    public string Content { get; set; }
}
```
## API
The API exposes 3 endpoints:
 - 1 endpoint where the user can send a request to send SMS
 - 1 endpoint to retrieve all SMSs
 - 1 endpoint to retrieve information about a specific SMS
### Send SMS
The user sends a POST request with:
 - `From` Originating number (can be anything)
 - `To` Destination number(s)
 - `Content` The content of the message
### Get All SMSs
Should return JSON array with all the SMSs (`Content` property should be removed from every message, `Status` property should be added).
### Get SMS
Should return a JSON object for a specific SMS (object should include `Status` property) or an error if the SMS can't be found.
## Console App
The console application is responsible for sending the message.
It should listen to SMS queue and process incoming messages.
When a message is being processed we should print the destination number to the console and update the status to `delivered` if the destination is a valid phone number, `failed` otherwise.
Processed message is saved into a database.
