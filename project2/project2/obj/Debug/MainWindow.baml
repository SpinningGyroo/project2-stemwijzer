   M S B A M L   `   `   `  ����C  ?project2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:project2project2  5   	       project2.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:project2     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � ��5
   
   �� 5       �� ImageSource$ images/background.jpg��6    � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6    � RowDefinitions 5   
   �� 5      $
��0.2* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $
��0.2* �6   .+�5    
   �� 
�������$��5��6$   $��4��64   
���   �$��1,0,2,1q�6U   ._�5!      � 5#      5$      �� 5%   
   $��1��6   $��3��6   $��2��6/   $��4��6?   
�������
���   �$��2,2,2,0q�6z    �� CornerRadius$ 
30,30,0,30��6�   ._�5&      �  5'      �� 5(      $��1* �6'   �� 5)      $��1* �6'   �� 5*      $��1* �6'    5,      �� 5-      $
��0.2* �6$   �� 5.      $��1* �6$   �� 5/      $��1* �6$   �� 50      $��1* �6$   �� 51      $
��0.2* �6$   .+�53      (� $
��Left=�6   $��1��65   $��1��6B   .��54      �� $
��Bold"�6    
StemWijzer6>   55      (� 56       (� Stretch$
 Fill=�6   $��2��6)   $��1��66   .��57      �� -   5        $��Start��57      $	��100��6-   $��40��69   $��16(�6E    � 	Resources 58       �� 
TargetType �� Property	 �� Value��%_   '��       �� 59      " ��6$   .�5:   "   ��  �@��CornerRadius$	 15��6A   5;      5<      ��5=      �� 5>      " ��6$   .�5?   "   ��  � ��	 �#4546546?   �� 5@   "    � ��
	 �����6?   �� 5A   "    � ��	 �#2021236@   �� 5B   "    � ��$	 2q�6D   5C      5E      5F      5G      5H      �� 5Q   
   -   5        
 ��Name$
 SignUp��5R      $��4��5T      $��2��5U      $��3��5V      $��3��5W      ���#454654
���   �$��1,1,1,1q�5Z      $ 	0,30,30,0��5[      ._�5\      � -   5         �Name$ 
gridSignUp��5]       5_      �� 5`      $
��0.1* �6'   �� 5a      $��1* �6'   �� 5b      $��1* �6'   �� 5c      $��1* �6'   �� 5d      $��1* �6'   �� 5e      $
��0.1* �6'    5g      �� 5h      $
��0.2* �6$   �� 5i      $��1* �6$   �� 5j      $��1* �6$   �� 5k      $��1* �6$   �� 5l      $��1* �6$   �� 5n      $
��0.2* �6$   .+�5q      (� $ Uniform=�6   $��4��6,   $��2��69   $��4��6I   .��5r      �� -   5         ��Name$ btnCreateUser��5r      $��60(�64   $
��Bold"�6B   
����������#454654���#454654.��6�   create user6�   5s      (� 5t      $��1��6   $��1��6'   $��2��67   $
��Left=�6K   .��5u      �� 
�������	username:6>   5v      (� 5w      $��2��6   $��1��6'   $��2��67   $
��Left=�6K   .��5x      �� 
�������	password:6>   5y      (� 5z      $��3��6   $��1��6'   $��2��67   $��Center=�6K   .��5{      �� 
�������	E-mail:6<   5|      (� 5~      $
 Fill=�6   $��2��6)   $��3��66   $��2��6F   .��5      G� -   5         G�Name$ txtPasswordCreate��5   "   $��8(�6=   $��80��6J   ���#4546546m   5�      (� 5�      $
 Fill=�6   $��1��6)   $��3��66   $��2��6F   .��5�      �� -   5         ��Name$ txtUsernameCreate��5�      $��80��69   $��8(�6D   ���#4546546i   5�      (� 5�      $
 Fill=�6   $��3��6)   $��3��66   $��2��6F   .��5�      �� -   5        $ txtEmailCreate��5�      $��80��66   $��8(�6A   ���#4546546f   5�      (� 5�      $��Right=�6   $
 Fill=�66   $��4��6E   $��1��6R   .��5�      �� -   5        $ 	btnGoBack��5�      
����������#454654���#454654$��50(�6�   .��6�   ←6�   5�      5�      5�      �� 5�   
   -	   5        $
 MainWindowBorder��5�      $��4��5�      $��2��5�      $��3��5�      $��3��5�      ���#454654
���   �$��1,1,1,1q�5�      $ 	0,30,30,0��5�      ._�5�      � -
   5        $ gridMainWindow��5�       5�      �� 5�      $
��0.1* �6'   �� 5�      $��1* �6'   �� 5�      $��1* �6'   �� 5�      $
��0.1* �6'    5�      �� 5�      $
��0.2* �6$   �� 5�      $��1* �6$   �� 5�      $��1* �6$   �� 5�      $��1* �6$   �� 5�      $��1* �6$   �� 5�      $
��0.2* �6$   .+�5�      (� $��1��6   $��1��6'   $
��Left=�67   .��5�      �� 
�������	username:6>   5�      (� 5�      $��2��6   $��1��6'   $
��Left=�67   .��5�      �� 
�������	password:6>   5�      (� 5�      $��Right=�6   $ Uniform=�66   $��3��6H   $��2��6U   .��5�      �� -   5        $ btnLogin��5�      
����������#454654���#454654.��6�   log in6�   5�      (� 5�      $
 Fill=�6   $��1��6)   $��2��66   .��5�      �� -   5        $ txtUsername��5�      $��80��63   $��8(�6>   ���#4546546c   5�      (� 5�      $
 Fill=�6   $��2��6)   $��2��66   .��5�      G� -   5        $ txtPassword��5�   "   $��8(�67   $��80��6D   ���#4546546g   5�      (� 5�      $ Uniform=�6   $��4��6,   $��1��69   $��2��6I   .��5�      �� -   5        $ 	btnSignup��5�      $��60(�60   $
��Bold"�6>   
����������#454654���#454654$��Register��6�   5�      5�      5�      �� 5�   
   -   5        $
 LoggedIn��5�      $��4��5�      $��2��5�      $��3��5�      $��3��5�      ���#454654
���   �$��0,2,2,2q�5�      $ 	0,30,30,0��5�      ._�5�      � -   5        $ gridLoggedIn��5�       5�      �� 5�      $
��0.1* �6'   �� 5�      $��1* �6'   �� 5�      $��1* �6'   �� 5�      $��1* �6'   �� 5�      $��1* �6'   �� 5�      $
��0.1* �6'    5�      �� 5�      $
��0.2* �6$   �� 5�      $��1* �6$   �� 5�      $��1* �6$   �� 5�      $��1* �6$   �� 5�      $��1* �6$   �� 5�      $
��0.2* �6$   .+�5�      (� $��2��5�      $��3��5�      $��2��5�      $
��Left=�5�      $��59,0,0,0q�5�      $��83��5�      .��5�      �� -   5        
����������#454654���#202123$��11(�5�   [   $��Right=�6i    5�      ��%^   '��       �� 5�      " ��6$   .�5�   "   ��  �@��CornerRadius$	 5��6A   5�      5�      .��6,   Full result5�      5�      (� 5�      $��4��5      $��4��5     $��2��5     .��5     �� -   5        
����������#454654���#454654$��8(�5     $��Right=�5	     $��18��5
  *   $��34��66   .��5     	Log out5     5     (� 5     $��1��5     $��3��5     $��1��5     $
��Left=�5     .��5     �� -   5        $ btnUploadImage��5     
����������#454654���#454654$��8(�5     $
��Left=�5      J� RenderTransformOrigin$ 0.5,0.5&�5  3   $��14��6S   z�5     V� 5      .~�5!  "   ��  �� ScaleX$ -1Q�61   �� 5"  "   �� 5#  "   T� 5$  "   5%     .��5&  2   ✎5(     5)     5+     5,     �� 54  
   $��1��6   $��2��6!   $��2��65   $��4��6B   ���#202123
���   �$��1,1,1,1q�6}   $ 
30,30,0,30��6�   ._�55     � -   5        $��gridPartijen��55      56     �� 57     $
��0.7* �6'   �� 58     $��1* �6'   �� 59     $��1* �6'   �� 5:     $��1* �6'   �� 5;     $
��0.7* �6'    5=     �� 5>     $
��0.2* �6$   �� 5?     $��1* �6$   �� 5@     $��1* �6$   �� 5A     $��1* �6$   �� 5B     $��1* �6$   �� 5C     $��1* �6$   �� 5D     $��1* �6$   �� 5E     $
��0.2* �6$   5G     5H     5I     5J     