# Dell Warranty Checker
This application was written due to a need to more quickly be able to look up warranty information on computers. It allows the user to enter in a computer name, service tag or IP address and then it will attempt to perform the lookup. If an IP address or computer name is provided then it will attempt to connect to the computer in question and pull the service tag information. If the service tag was able to be retreived or if it was manually entered then the applicaiton will use the Dell API to pull the warranty information.

# Credits
Thanks to [newtonsoft](https://www.newtonsoft.com/) for the [.net JSON framework.](https://www.newtonsoft.com/json)
