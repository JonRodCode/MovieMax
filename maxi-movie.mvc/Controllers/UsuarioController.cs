using maxi_movie.mvc.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace maxi_movie.mvc.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public UsuarioController(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel usuario)
        {
            if(ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(usuario.Email, usuario.Clave, usuario.Recordarme, lockoutOnFailure: false);
                if(resultado.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Intento de inicio de sesión no válido.");
                }
            }

            return View();
        }
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(UsuarioViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                var nuevoUsuario = new Usuario
                {
                    UserName = usuario.Email,
                    Email = usuario.Email,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    ImagenUrlPerfil = "default-profile.png"
                };
                var resultado = _userManager.CreateAsync(nuevoUsuario, usuario.Clave);
                if (resultado.Result.Succeeded)
                {
                    await _signInManager.SignInAsync(nuevoUsuario, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in resultado.Result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(usuario);
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
