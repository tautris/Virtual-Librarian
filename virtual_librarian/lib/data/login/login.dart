import 'package:camera/camera.dart';

abstract class LoginRepository {
  Future <List<CameraDescription>> fetchCameras();
}

class FetchCamerasException implements Exception {
  final _message;

  FetchCamerasException([this._message]);

  String toString() {
    if (_message == null) return "Exception";
    return "Exception: $_message";
  }
}