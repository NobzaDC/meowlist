import { NavLink } from "react-router-dom"
import {
  Sidebar,
  SidebarContent,
  SidebarGroup,
  SidebarGroupContent,
  SidebarGroupLabel,
  SidebarMenu,
  SidebarMenuButton,
  SidebarMenuItem,
} from "@/components/ui/sidebar"

// Interfaz para los items del sidebar
export interface SidebarItem {
  title: string;
  url: string;
  key: string;
  icon?: React.ComponentType<{ size?: number }>;}
  
// Recibe items como prop
export function AppSidebar({ items }: { items: SidebarItem[] }) {
  return (
    <Sidebar>
      <SidebarContent>
        <SidebarGroup>
          <SidebarGroupContent>
            <SidebarMenu>
              {items.map((item) => (
                <SidebarMenuItem key={item.title}>
                  <SidebarMenuButton asChild>
                    <NavLink
                      to={item.url}
                      className={({ isActive }) =>
                        `flex items-center gap-2 px-2 py-1 rounded transition ${
                          isActive ? "bg-primary text-onPrimary" : "hover:bg-primary/10"
                        }`
                      }
                    >
                      {/* Iconos opcionales */}
                      {item.icon && <item.icon />}
                      <span>{item.title}</span>
                    </NavLink>
                  </SidebarMenuButton>
                </SidebarMenuItem>
              ))}
            </SidebarMenu>
          </SidebarGroupContent>
        </SidebarGroup>
      </SidebarContent>
    </Sidebar>
  )
}