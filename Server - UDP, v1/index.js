// ------------------------------ setup the server ------------------------------
var express = require('express');
var app = express();
var server = require('http').createServer(app);
var path = require('path');
app.use(express.static(path.join(__dirname, '')));

//server variables
var currentBNO055Value = 0;
var currentKeyPadValue = 0;

//start server
server.listen(3000, function () {
    console.log("Server listening on port: 3000\n");
});

//on HTTP GET "/" (e.g.: http://localhost:3000/)
app.get('/', function (req, res) {
    //send index.html to client browser
    res.sendFile(__dirname + '/index.html');
});

// ------------------------------ Find and print the server IP-address ------------------------------ 
var networkInterfaces = require('os').networkInterfaces();
var address;

Object.keys(networkInterfaces).forEach(function(interface) {
    networkInterfaces[interface].filter(function(details) {
        if (details.family === 'IPv4' && details.internal === false) {
            address = details.address;
        }
    });
});

console.log("Server IP-address: " + address + "\n");

// ------------------------------ setup UDP (user datagram protocol) ------------------------------ 
var UPD = require("dgram");
var UDPsocket = UPD.createSocket('udp4');

//set the port for which to listen for UDP messages on
UDPsocket.bind(3001);

//store arduino IP info
var picoIPAddress;
var picoPort;

//on error
UDPsocket.on('error', function (error) {
    console.error("UDP error: " + error.stack + "\n");
});

//on begin listening
UDPsocket.on('listening', function () {
    var listenAtPort = UDPsocket.address().port;
    console.log('Server listening for UDP packets at port: ' + listenAtPort + "\n");
});

//on received message
UDPsocket.on('message', (msg, senderInfo) => {
    console.log("Received UDP message: " + msg);
    console.log("From addr: " + senderInfo.address + ", at port: " + senderInfo.port + "\n");
    var message = msg.toString()

    if (message.length > 10) {
        currentBNO055Value = message
        EmitBNO055Value();
    } else {
        currentKeyPadValue = message
        EmitKeyPadValue();
    }

    picoIPAddress = senderInfo.address;
    picoPort = senderInfo.port;
});

// ------------------------------ setup Socket.IO ------------------------------ 
var io = require('socket.io')(server);

//on new client connection
io.on('connection', function(IOsocket) {
    console.log("Client has connected" + "\n");

    //on client disconnection
    IOsocket.on('disconnect', function () {
        console.log("Client has disconnected" + "\n");
    });
    //on "UpdateCurrentKeyPadValue"
    IOsocket.on('UpdateKeyPadValue', function (data) {
        console.log("Current KeyPad Value received from client: " + data + "\n");
        currentKeyPadValue = data;

        io.emit('CurrentKeyPadValue', currentKeyPadValue);

        //If arduino, send LED value with UDP
        //if (arduinoIPAddress != null && arduinoPort != null) {
        //    sendUDPMessage(arduinoIPAddress, arduinoPort, currentKeyPadValue)
        //}
    });
});

//emit "CurrentPotentiometerValue"
function EmitBNO055Value() {
    io.emit('CurrentBNO055Value', currentBNO055Value);
}

function EmitKeyPadValue() {
    io.emit('CurrentKeyPadValue', currentKeyPadValue);
}