import { Link } from "react-router-dom";

export default function NotFound() {
  return (
    <div className="min-h-screen bg-background flex flex-col items-center justify-center">
      <h1 className="text-5xl font-heading text-error mb-4">404</h1>
      <h2 className="text-2xl font-heading text-primary mb-2">Página no encontrada</h2>
      <p className="text-lg text-text-secondary mb-6">
        La página que buscas no existe o fue movida.
      </p>
      <Link to="/" className="text-primary underline text-lg">
        Volver al inicio
      </Link>
    </div>
  );
}