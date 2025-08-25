import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Button } from "@/components/ui/button"
import {
    Card,
    CardContent,
    CardFooter,
    CardHeader,
    CardTitle,
    CardDescription,
    CardAction
} from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { GoArrowLeft } from "react-icons/go";

function Register() {
    const navigate = useNavigate();

    const [name, setName] = useState("");
    const [lastName, setLastName] = useState("");
    const [email, setEmail] = useState("");
    const [user, setUser] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        // TODO: lógica de register aquí
        navigate("/main");
    };

    return (
        <div className="min-h-screen bg-primary flex items-center justify-center">
            <Card className="w-full max-w-sm">
                <CardHeader className="relative px-6 flex flex-col items-center">
                    <CardAction
                        className="absolute left-0 top-6 cursor-pointer pl-4"
                        onClick={() => navigate("/")}
                    >
                        <GoArrowLeft size={28} />
                    </CardAction>
                    <div className="flex flex-col items-center w-full">
                        <CardTitle>
                            <img src="/logo_black.svg" alt="Meowlist Logo" className="w-72 h-32 mb-6 object-contain" />
                            <h1 className="text-5xl font-heading text-primary mb-4 text-center">Sign up!</h1>
                        </CardTitle>
                        <CardDescription className="text-center">
                            <Link to="/" className="text-primary underline">
                                Already have an account? Log in
                            </Link>
                        </CardDescription>
                    </div>
                </CardHeader>
                <CardContent>
                    <form onSubmit={handleSubmit}>

                        <div className="grid grid-cols-2 grid-rows-4 gap-4">
                            <div>
                                <Label htmlFor="name">Name</Label>
                                <Input
                                    id="name"
                                    type="text"
                                    placeholder="Your name"
                                    value={name}
                                    onChange={(e) => setName(e.target.value)}
                                    required
                                />
                            </div>
                            <div >
                                <Label htmlFor="last">Last Name</Label>
                                <Input
                                    id="last"
                                    type="text"
                                    placeholder="Your last name"
                                    value={lastName}
                                    onChange={(e) => setLastName(e.target.value)}
                                    required
                                />
                            </div>
                            <div >
                                <Label htmlFor="email">Email</Label>
                                <Input
                                    id="email"
                                    type="email"
                                    placeholder="Your email"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    required
                                />
                            </div>
                            <div >
                                <Label htmlFor="user">User</Label>
                                <Input
                                    id="user"
                                    type="text"
                                    placeholder="Your user"
                                    value={user}
                                    onChange={(e) => setUser(e.target.value)}
                                    required
                                />
                            </div>
                            <div className="col-span-2">
                                <Label htmlFor="password">Password</Label>
                                <Input
                                    id="password"
                                    type="password"
                                    placeholder="Your password"
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    required
                                />
                            </div>
                            <div className="col-span-2 row-start-4">
                                <Label htmlFor="confirm-password">Confirm Password</Label>
                                <Input
                                    id="confirm-password"
                                    type="password"
                                    placeholder="Your password"
                                    value={confirmPassword}
                                    onChange={(e) => setConfirmPassword(e.target.value)}
                                    required
                                /></div>
                        </div>

                        <CardFooter className="flex-col gap-2 mt-6 pl-0 pr-0">
                            <Button type="submit" className="w-full">
                                Sign up
                            </Button>
                        </CardFooter>
                    </form>
                </CardContent>
            </Card>
        </div>
    );
}

export default Register;
