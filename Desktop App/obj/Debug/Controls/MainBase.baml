   M S B A M L   `   `   `  ����J  FMenhely Projekt, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Menhely_Projekt.ControlsMenhely_Projekt.Controls  5      '    !Menhely_Projekt.Controls.MainBase   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      8local&clr-namespace:Menhely_Projekt.Controls     5      .��5	      ]� .+�5   
   )   #Menhely_Projekt.Controls.NavControl  -   5           Name$  NavBar��5   T   $��50��6   $	��Top=�6'   $	��Top=�6<   6T   � 5   
   $��10q�6    � ColumnDefinitions 5      �� 5      $	��250 �6#   �� 5      $��* �6#    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6   $��1��6)   ���#E0E0E0$��0,0,10,0q�6K   .+�5      �� -   5         ��Name$ 	Reset_btn��5      $��50��6,   $	��200��68   $
��🔁��6D   $��32(�6Q   $
��Left=�5      $��
20,10,0,10q�60   
����  �6\   �� 5!      -   5        $ ModifyKutya_btn��5!      $��50��62   $	��200��6>   $��Kutya Módosítása��6J   $
��Left=�5"      $��
20,10,0,10q�60   $��18(�6b   
���� �6p   �� 5%      -   5        $ AddKutya_btn��5%      $��50��6/   $	��200��6;   $��Új kutya hozzáadása��6G   $
��Left=�5&      $��
20,10,0,10q�60   $��18(�6_   
���� �6m   �� 5)      -   5        $ 
Kennel_btn��5)      $��50��6-   $	��200��69   $��Kennelek kezelése��6E   $
��Left=�5*      $��
20,10,0,10q�60   $��18(�6]   
���� �6k   �� 5-      -   5        $ History_btn��5-      $��50��6.   $	��200��6:   $��Legutóbbi módosítások��6F   $
��Left=�5.      $��
20,10,0,10q�60   $��Bottom=�6D   $��18(�6y   
���� �6�   �� 51      -   5        $ 
Logout_btn��51      $��50��6-   $	��200��69   $��Kijelentkezés��6E   $
��Left=�52      $��
20,10,0,10q�60   $��18(�53   .   
���  ��$��Bottom=�6M   54      � 57      $��1��6   $��0��6#   $��0,0,0,10q�60    58      �� 59      $
��Auto �6'   �� 5:      $��* �6'   .+�5<      �� -   5         ��Name$ 	Kereso_cb��5<   I   $��0��6   $	��150��6+   $��0,0,10,0q�67   $��16(�6�   �� 5=      -	   5        $ 
Options_cb��5=      $��1��6/   $��Stretch=�6?   6]   �� 5>      -
   5         ��Name$ 	Kereso_tb��5>      $��1��6-   $��Stretch=�6=   $��16(�6s   5?      � 5B      $��1��6   $��1��6#   .+�5C      &   System.Windows.Controls.DataGrid  -   5          Name$ KutyaDataGrid��5C         AutoGenerateColumns .    CanUserAddRows .  $��Stretch=�5F      $��Stretch=�5G      	   
IsReadOnly	 . $��16(�5H   c   5J      5K      5L      5M      5N      