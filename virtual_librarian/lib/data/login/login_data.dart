import 'package:camera/camera.dart';
import 'package:virtual_librarian/data/login/login.dart';

class LoginRepositoryData implements LoginRepository {
  @override
  Future<List<CameraDescription>> fetchCameras() async {
    List<CameraDescription> cameras = new List();
    cameras = await availableCameras();
    return cameras;
  }
}