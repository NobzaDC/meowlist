import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button"
import {
  Card,
  CardContent,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { loginUser } from "@/services/Actions/User/LoginUser";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);
    const response = await loginUser({ email, password });
    if (response.code === 200 && response.value) {
      localStorage.setItem("token", response.value.access_token);
      navigate("/main");
    } else {
      setError(response.message || "Login failed");
    }
  };

  return (
    <div className="min-h-screen bg-primary flex items-center justify-center">
      <Card className="w-full max-w-sm">
        <CardHeader>
          <CardTitle className="flex flex-col items-center">
            <img src="/logo_black.svg" alt="Meowlist Logo" className="w-72 h-32 mb-6 object-contain" />
            <h1 className="text-5xl font-heading text-primary mb-4">Moewvenido!</h1>
          </CardTitle>
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit}>
            <div className="flex flex-col gap-6">
              <div className="grid gap-2">
                <Label htmlFor="email">Email</Label>
                <Input
                  id="email"
                  type="email"
                  placeholder="email@catmail.com"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required
                />
              </div>
              <div className="grid gap-2">
                <div className="flex items-center">
                  <Label htmlFor="password">Password</Label>
                  <a
                    href="#"
                    className="ml-auto inline-block text-sm underline-offset-4 hover:underline"
                  >
                    Forgot your password?
                  </a>
                </div>
                <Input id="password" type="password" placeholder="********" required value={password} onChange={(e) => setPassword(e.target.value)} />
              </div>
              {error && (
                <div className="text-error text-sm text-center">{error}</div>
              )}
            </div>
            <CardFooter className="flex-col gap-2 mt-6 pl-0 pr-0">
              <Button type="submit" className="w-full">
                Login
              </Button>
              <p className="text-sm text-text-secondary">
                Not registered?{" "}
                <Link to="/register" className="text-primary underline">
                  Sign up
                </Link>
              </p>
            </CardFooter>
          </form>
        </CardContent>
      </Card>
    </div>
  );
}

export default Login;
