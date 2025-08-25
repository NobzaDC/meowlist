import { useLocation, Outlet } from "react-router-dom";
import { AppSidebar, type SidebarItem } from "@/components/sections/app-sidebar";
import {
  Breadcrumb,
  BreadcrumbLink,
  BreadcrumbList,
  BreadcrumbPage,
  BreadcrumbSeparator,
} from "@/components/ui/breadcrumb";
import { Separator } from "@/components/ui/separator";
import {
  SidebarInset,
  SidebarProvider,
  SidebarTrigger,
} from "@/components/ui/sidebar";

// Sidebar menu items
const menuItems: SidebarItem[] = [
  { title: "Home", url: "/main", key: "home" },
  { title: "My Day", url: "/main/myday", key: "myday" },
  { title: "Calendar", url: "/main/calendar", key: "calendar" },
  { title: "Tags", url: "/main/tags", key: "tags" },
  { title: "Settings", url: "/main/settings", key: "settings" },
];

// Breadcrumbs helper
function getBreadcrumbs(pathname: string) {
  const crumbs = [];
  const segments = pathname.split("/").filter(Boolean);

  let url = "";
  
  for (let i = 0; i < segments.length; i++) {
    url += "/" + segments[i];
    const menuItem = menuItems.find((item) => item.url === url);
    crumbs.push({
      label: menuItem ? menuItem.title : segments[i],
      url,
      isLast: i === segments.length - 1,
    });
  }
  return crumbs.length
    ? crumbs
    : [{ label: "Home", url: "/main", isLast: true }];
}

export default function MainPage() {
  const location = useLocation();
  const breadcrumbs = getBreadcrumbs(location.pathname);

  return (
    <SidebarProvider>
      <AppSidebar items={menuItems} />
      <SidebarInset>
        <header className="flex h-16 shrink-0 items-center gap-2 border-b px-4">
          <SidebarTrigger className="-ml-1" />
          <Separator
            orientation="vertical"
            className="mr-2 data-[orientation=vertical]:h-4"
          />
          <Breadcrumb>
            <BreadcrumbList>
              {breadcrumbs.map((crumb, idx) => (
                <span key={crumb.url} className="flex items-center">
                  {crumb.isLast ? (
                    <BreadcrumbPage>{crumb.label}</BreadcrumbPage>
                  ) : (
                    <BreadcrumbLink href={crumb.url}>{crumb.label}</BreadcrumbLink>
                  )}
                  {idx < breadcrumbs.length - 1 && <BreadcrumbSeparator />}
                </span>
              ))}
            </BreadcrumbList>
          </Breadcrumb>
        </header>
        <div className="flex flex-1 flex-col gap-4 p-4">
          <Outlet />
        </div>
      </SidebarInset>
    </SidebarProvider>
  );
}