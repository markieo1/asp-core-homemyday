//Load YouTube script API
//This method is necessary to load the script asynchronously.
var scriptElement = document.createElement("script");
scriptElement.src = "http://www.youtube.com/iframe_api";
var firstScriptElement = document.getElementsByTagName("script")[0];
firstScriptElement.parentNode.insertBefore(scriptElement, firstScriptElement);

let callbacks = [];
let ready = false;

//This function will be executed when the YouTube iframe API is ready.
function onYouTubeIframeAPIReady()
{
    ready = true;
    //Execute every added callback
    for(let callback of callbacks)
    {
        callback();
    }
}

/**
 * Add a callback to be executed when the YouTube API is ready.
 * If the API is already ready, the callback will be executed immediately.
 * @param {function} callback The callback to execute.
 */
function addYouTubeApiReady(callback)
{
    if(ready)
    {
        callback();
    }
    else
    {
        callbacks.push(callback);
    }
}