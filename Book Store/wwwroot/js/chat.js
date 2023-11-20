"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/openAI")
    .configureLogging(signalR.LogLevel.Information)
    .build();

async function start() {
    await connection.start().then(async function () {
        document.getElementById("chatButton").disabled = false;
        const chatLog = await fetch('/api/Chat/GetChatLog');

        chatLog.json().then(data => {
            for (let i = 0; i < data.length; i++) {
                if (data[i].role == "Assistant")
                    createReceiveMessageDiv(data[i].content);
                else
                    createSendMessageDiv(data[i].content);
            }
            chatBody.scrollTop = chatBody.scrollHeight;
        })
    }).catch(function (err) {
        console.error(err.message);
        setTimeout(start, 5000);
    });
}

connection.onclose(async () => {
    await start();
})

start();
const chatLog = [];

//Disable the send button until connection is established.
document.getElementById("chatButton").disabled = true;

function createReceiveMessageDiv(message) {
    var chatBody = document.getElementById("chatBody");

    // Create a message block
    var receiveMessageDiv = document.createElement("div");
    receiveMessageDiv.classList.add("d-flex", "flex-row", "justify-content-start", "mb-4");

    // Create avatar image
    var avatar = document.createElement("img");
    avatar.src = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava5-bg.webp";
    avatar.alt = "Image Not Found!";
    avatar.style.width = "45px";
    avatar.style.height = "100%";

    // Create message content 
    var content = document.createElement("p");
    content.classList.add("small", "p-2", "ms-3", "mb-1", "rounded-3");
    content.style.backgroundColor = "#f5f6f7";
    content.innerHTML = `${message}`;

    // Assemble all elements
    var div = document.createElement('div');
    div.appendChild(content);
    receiveMessageDiv.appendChild(avatar);
    receiveMessageDiv.appendChild(div);
    chatBody.appendChild(receiveMessageDiv);
}

function createSendMessageDiv(message) {
    // Add new message to chatbox
    var chatBody = document.getElementById("chatBody");

    //Create a message block
    var newMessageDiv = document.createElement("div");
    newMessageDiv.classList.add("d-flex", "flex-row", "justify-content-end", "mb-4");

    // Create avatar image
    var avatar = document.createElement("img");
    avatar.src = "https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava2-bg.webp";
    avatar.alt = "Image Not Found!";
    avatar.style.width = "45px";
    avatar.style.height = "100%";

    // Create message content
    var content = document.createElement("p");
    content.classList.add("small", "p-2", "me-3", "mb-1", "text-white", "rounded-3", "bg-info");
    content.innerHTML = `${message}`;

    // Assemble all elements
    var div = document.createElement('div');
    div.appendChild(content);
    newMessageDiv.appendChild(div);
    newMessageDiv.appendChild(avatar);
    chatBody.appendChild(newMessageDiv);
}

connection.on("RecieveMessage", function (message) {
    createReceiveMessageDiv(message);
    chatBody.scrollTop = chatBody.scrollHeight;
});

document.getElementById("exampleFormControlInput3").addEventListener("keypress", function (event) {
    if (event.key == "Enter")
        sendMessage2Server();
})
document.getElementById("sendMessage").addEventListener("click", function (event) {
    sendMessage2Server();
});

function sendMessage2Server() {
    var message = document.getElementById("exampleFormControlInput3").value;
    var user = document.getElementById("UserName").innerText;

    createSendMessageDiv(message);
    chatBody.scrollTop = chatBody.scrollHeight;

    // Send new message to OpenAI Server
    connection.invoke("SendMessageAsync", user, message).catch(function (err) {
        console.log(err);
        return console.error(err.toString());

    });

    document.getElementById("exampleFormControlInput3").value = "";
    //chatLog.push({
    //    Role: "User",
    //    Message: message
    //});
    event.preventDefault();
}

function getChatLog() {
    var chatBody = document.getElementById("chatBody");
}