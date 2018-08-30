var _JS_UNETWebSockets_SocketCreate_Original = _JS_UNETWebSockets_SocketCreate;
_JS_UNETWebSockets_SocketCreate = function (hostId, urlPtr) {
  var url = Pointer_stringify(urlPtr).replace(/^ws:\/\//, "wss://");
  urlPtr = stackAlloc((url.length << 2) + 1);
  writeAsciiToMemory(url, urlPtr);
  return _JS_UNETWebSockets_SocketCreate_Original(hostId, urlPtr);
};
