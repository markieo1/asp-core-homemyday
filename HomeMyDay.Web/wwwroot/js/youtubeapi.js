//Load YouTube script API
//This method is necessary to load the script asynchronously.
var scriptElement = document.createElement("script");
scriptElement.src = "http://www.youtube.com/iframe_api";
var firstScriptElement = document.getElementsByTagName("script")[0];
firstScriptElement.parentNode.insertBefore(scriptElement, firstScriptElement);