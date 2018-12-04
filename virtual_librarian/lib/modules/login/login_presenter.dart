import 'package:camera/camera.dart';
import 'package:virtual_librarian/data/dependency_injection.dart';
import 'package:virtual_librarian/data/login/login.dart';

abstract class LoginViewContract {
  void onLoadCameraComplete(List<CameraDescription> cameras);
  void onLoadCameraError();
}

class LoginPresenter {
  LoginViewContract _view;
  LoginRepository _repository;

  LoginPresenter(this._view) {
    _repository = new Injector().loginRepository;
  }

  void loadCameras() {
    _repository
      .fetchCameras()
      .then((cameras) => _view.onLoadCameraComplete((cameras)))
      .catchError((onError) => _view.onLoadCameraError());
  }
}